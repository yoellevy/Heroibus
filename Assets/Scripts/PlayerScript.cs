using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private Character _player;

    [SerializeField]
    private GameObject healthyBodyPart, deadBodyPart;

    [SerializeField]
    public Animator _playerPartsAnimator;
    public Animator _playerStarAnimator;
    public Animator _playerHealthAnimator;    

    public GameObject party, otherParty;

    public int currAtkInd, currPotionInd;

    public bool inFront;

    private TurnBasedSystem myTBS;


    private void Awake()
    {
        this.party = this.gameObject.transform.parent.gameObject;
        _player.HP = _player.MaxHP;
        _player.MP = _player.MaxMP;
        _player.Speed = _player.DefaultSpeed;

        if (this.gameObject.tag == "PlayerUnit")
            inFront = true;
        else
            inFront = false;

        if (party.name == "PlayerParty")
        {
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void SetCurrAttack(int i)
    {
        currAtkInd = i;
    }

    public void SetCurrPotion(int i)
    {
        currPotionInd = i;
    }

    //TODO: Is this needed????
    public List<Attack> Attacks { get { return _player.Attacks; } }
    public List<Potion> Potions { get { return _player.Potions; } }
    public float HP { get { return _player.HP; } set { _player.HP = value < 0 ? 0 : value; } }
    public float MP { get { return _player.MP; } set { _player.MP = value < 0 ? 0 : value; } }
    public float Speed { get { return _player.Speed; } set { _player.Speed = value < 0 ? 0 : value; } }
    public float Def { get { return _player.Def; } set { _player.Def = value < 0 ? 0 : value; } }
    public int Exp { get { return _player.Exp; } }


    public void AddExp(int gainedExp)
    {
        _player.Exp += gainedExp;
    }


    private void UpdateDmgHUD(float textToShow, float moveByUp, float moveByLeft)
    {
        GameObject HUDCanvas = GameObject.Find("HUDCanvas");
        Vector3 pos = new Vector3(this.transform.position.x - moveByLeft, this.transform.position.y + moveByUp, this.transform.position.z);
        GameObject damageText = (GameObject)Instantiate(Resources.Load("damageTextPrefab"), pos, Quaternion.identity, HUDCanvas.transform);
        damageText.GetComponent<Text>().text = "" + textToShow;
    }

    public void UpdateInfoHUD(GameObject target, string textToShow, float moveUpBy, float moveLeftBy)
    {
        GameObject HUDCanvas = GameObject.Find("HUDCanvas");
        Vector3 pos = new Vector3(target.transform.position.x - moveLeftBy, target.transform.position.y + moveUpBy, target.transform.position.z);
        GameObject infoText = (GameObject)Instantiate(Resources.Load("InfoTextPrefab"), pos, Quaternion.identity, HUDCanvas.transform);
        infoText.GetComponent<Text>().text = "" + textToShow;
    }

    private void KillUnit()
    {
        if (this.gameObject.tag == "CompanionUnit" || this.gameObject.tag == "PlayerUnit")
        {
            myTBS = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
            myTBS.shouldTurnOffFlips = 1;
        }
        this.gameObject.tag = "DeadUnit";

        if (healthyBodyPart && deadBodyPart)
        {
            _playerPartsAnimator.SetBool("dead", true);
        }

        if (party.name == "EnemyUnits")
        {
            GameObject nextAvailableEnemy = GameObject.FindGameObjectWithTag("EnemyUnit");
            if (nextAvailableEnemy)
            {
                party.GetComponent<SelectUnit>().SetAttackTarget(nextAvailableEnemy);
            }
        }
    }

    private float CalculateMissChance(GameObject target)
    {
        float speedDiff = target.GetComponent<PlayerScript>().Speed - this.gameObject.GetComponent<PlayerScript>().Speed;
        if (speedDiff < 0)
            speedDiff = 0;
        else if (speedDiff > 10)
            speedDiff = 10;
        return speedDiff / 10;
    }

    public IEnumerator WaitForNextTurn()
    {
        yield return new WaitForSeconds(1.2f);
        myTBS.NextTurn();
    }

    public bool TryHit(GameObject target)
    {
        float missChance = CalculateMissChance(target);
        float randomNumber = Random.value;
        if (randomNumber < missChance)
        {
            // miss
            return false;
        }
        else
        {
            return true;
        }
    }    

    public void UseAttack(GameObject target, int i)
    {
        PlayerScript targetPlayerScript = target.GetComponent<PlayerScript>();
        PartyStats currPartyStats = party.GetComponent<PartyStats>();
        Attack chosenAtk = _player.Attacks[i];
        myTBS = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();

        // Handling damage:
        if (currPartyStats.partyMP >= chosenAtk.MpDemand)
        {
            currPartyStats.partyMP -= chosenAtk.MpDemand;

            // signal animation to start
            StartCoroutine(playPlayerAnimation("isAttack"));
            if (chosenAtk.MpDemand > 0 && _playerStarAnimator)
            {
                StartCoroutine(playStarAnimation("changedAttribute"));
            }

            // Activating effects:
            for (int k = 0; k < chosenAtk.Effects.Length; k++)
            {
                if (myTBS.activeEffects.Find(myItem => myItem.name == chosenAtk.Effects[k].name))
                {
                    // already active:
                    chosenAtk.Effects[k].UpdateDuration();
                }
                else
                {
                    chosenAtk.Effects[k].DoAction();
                    myTBS.activeEffects.Add(chosenAtk.Effects[k]);
                }
            }

            if (chosenAtk.IsPureEffect)
            {
                myTBS.NextTurn();
            }
            else
            {
                bool didHit = TryHit(target);
                if (!didHit)
                {
                    if (target.name == "Boss")
                        UpdateInfoHUD(target, "Miss!", 1.6f, 0.9f);
                    else
                    {
                        UpdateInfoHUD(target, "Miss!", 1.6f, 0.0f);
                    }

                    StartCoroutine(WaitForNextTurn());
                }
                else
                {
                    float enhancement = Random.Range(chosenAtk.LowerRangeEnhancer, chosenAtk.UpperRangeEnhancer);
                    float baseWeaponDmg = Random.Range(_player.Weapon.DmgMin, _player.Weapon.DmgMax);
                    float dmg = (baseWeaponDmg + _player.Strength) * enhancement;
                    // Rounding to a whole number:
                    dmg = Mathf.Round(dmg);
                    float criticalProbVal = Random.value;
                    if (criticalProbVal <= chosenAtk.CriticalEnhancementProb)
                    {
                        if (target.name == "Boss")
                            UpdateInfoHUD(target, "Critical", 2.7f, 0.9f);
                        else
                            UpdateInfoHUD(target, "Critical", 2.7f, 0.0f);

                        dmg *= chosenAtk.CriticalEnhancementRatio;
                    }
                    // float attackMultiplier = (Random.value * (this.maxAttackMultiplier - this.minAttackMultiplier)) + this.minAttackMultiplier;        
                    targetPlayerScript.ReceiveDamage(dmg);
                }
            }
        }
        else
        {
            UpdateInfoHUD(this.gameObject, "Missing Star Power", 4.0f, 0.0f);
            myTBS.PlayTurn(this.gameObject);
        }
    }
    IEnumerator playStarAnimation(string animationName)
    {
        _playerStarAnimator.SetBool(animationName, true);
        yield return new WaitForSeconds(1);
        _playerStarAnimator.SetBool(animationName, false);
    }

    IEnumerator playPlayerAnimation(string animationName)
    {
        _playerPartsAnimator.SetBool(animationName, true);
        yield return new WaitForSeconds(1);
        _playerPartsAnimator.SetBool(animationName, false);
    }

    IEnumerator playHealthAnimation(string animationName)
    {
        _playerHealthAnimator.SetBool(animationName, true);
        yield return new WaitForSeconds(1);
        _playerHealthAnimator.SetBool(animationName, false);
    }

    // return a string with the added hp
    public string UsePotion(PlayerScript potionUser)
    {
        PartyStats partyStats = this.gameObject.transform.parent.gameObject.GetComponent<PartyStats>();
        Potion currPotion = potionUser.Potions[potionUser.currPotionInd];

        bool shouldRevive = false;

        if (this.HP <= 0 && currPotion.HPBoost > 0)
            shouldRevive = true;

        string toReturn = this.GainHP(currPotion.HPBoost);
        if (toReturn == "0")
            toReturn = currPotion.MPBoost.ToString() + " MP gained";
        else
        {
            StartCoroutine( playHealthAnimation("HealthUp"));
        }
        partyStats.partyMP += currPotion.MPBoost;

        if (shouldRevive)
        {
            _playerPartsAnimator.SetBool("dead", false);
            if (healthyBodyPart && deadBodyPart)
            {
                healthyBodyPart.SetActive(true);
                deadBodyPart.SetActive(false);
            }

            myTBS = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
            if (this.name == "Player")
            {
                this.tag = "PlayerUnit";
                myTBS.shouldTurnOffFlips = -1;
            }
            else if (this.name == "Companion")
            {
                this.tag = "CompanionUnit";
                myTBS.shouldTurnOffFlips = -1;
            }
        }
        return toReturn;
    }


    public void ReceiveDamage(float damage)
    {
        float dmgToBeDealt = damage - _player.Def;
        if (dmgToBeDealt < 0)
            dmgToBeDealt = 0;
        _player.HP -= dmgToBeDealt;

        if (this.gameObject.name == "Boss")
            UpdateDmgHUD(dmgToBeDealt, 1.6f, 0.9f);
        else
            UpdateDmgHUD(dmgToBeDealt, 1.6f, 0.0f);

        if (dmgToBeDealt > 0)
        {
            StartCoroutine(playHealthAnimation("HealthDown"));
            StartCoroutine(playPlayerAnimation("gotHit"));
        }
            

        if (_player.HP <= 0)
        {
            _player.HP = 0;
            KillUnit();
        }
    }

    // return a string with the added hp
    public string GainHP(float addedHP)
    {
        float boostToPrint = addedHP;
        if (_player.HP + addedHP >= _player.MaxHP)
        {
            boostToPrint = _player.MaxHP - _player.HP;
            _player.HP = _player.MaxHP;
        }
        else
        {
            _player.HP += addedHP;
        }
        if (boostToPrint > 0)
            return boostToPrint.ToString() + " HP gained";
        return "0";
    }
}
