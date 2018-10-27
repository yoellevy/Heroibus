using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TurnBasedSystem : MonoBehaviour {

    private const float MAX_POSSIBLE_MP = 1000f;

    public List<PlayerScript> fighters;
    public List<Effect> activeEffects;
    GameObject flipBtnCompanion;
    GameObject flipBtnPlayer;
    private GameObject playerParty;
    private GameObject enemyUnits;
    private GameObject playerUnit;
    private GameObject companionUnit;
    private Text playerMP, playerHP, companionHP, bossHP;
    private int expGained;
    private bool batttleEnded;
    public int shouldTurnOffFlips;

    [SerializeField]
    private GameObject actionsMenu, actionsMenuCompanion;

    [SerializeField]
    private GameObject youDied, youWon;

    void Start()
    {
        BackgroundMusicController.instance.PlayClip(2);
        InitializeFields();

        // Setup Player Units:
        AddUnits(playerParty, true);
        UpdateHPHUD(playerParty);
        UpdateMPHUD(playerParty);

        // Setup Enemy Units:
        AddUnits(enemyUnits, false);

        fighters = fighters.OrderByDescending(x => x.Speed).ToList();

        this.actionsMenu.SetActive(false);
        this.actionsMenuCompanion.SetActive(false);

        this.NextTurn();
    }

    private void InitializeFields()
    {
        fighters = new List<PlayerScript>();
        activeEffects = new List<Effect>();
        playerParty = GameObject.FindGameObjectWithTag("PlayerParty");
        enemyUnits = GameObject.FindGameObjectWithTag("EnemyUnits");
        playerHP = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Text>();
        companionHP = GameObject.FindGameObjectWithTag("CompanionHP").GetComponent<Text>();
        playerMP = GameObject.FindGameObjectWithTag("PlayerMP").GetComponent<Text>();
        bossHP = GameObject.FindGameObjectWithTag("BossHP").GetComponent<Text>();
        flipBtnPlayer = GameObject.Find("FlipBtnP");
        flipBtnCompanion = GameObject.Find("FlipBtnC");
        expGained = 0;
        batttleEnded = false;
        shouldTurnOffFlips = 0; // 0 means don't do anything
    }

    private void AddUnits(GameObject units, bool shouldSetSpeed)
    {
        PlayerScript unitScript;

        foreach (Transform unitTransform in units.transform)
        {
            unitScript = unitTransform.gameObject.GetComponent<PlayerScript>();
            fighters.Add(unitScript);
            if (shouldSetSpeed)
            {
                unitScript.Speed = units.GetComponent<PartyStats>().partySpeed;
            }
        }
        flipBtnCompanion.SetActive(false);
    }

    public IEnumerator WaitForLoseBattle()
    {
        youDied.SetActive(true);
        batttleEnded = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Title");
    }

    public IEnumerator WaitForWinBattle()
    {
        youWon.SetActive(true);
        AddPartyEXP();
        batttleEnded = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Title");
    }

    public IEnumerator WaitForMonsterTurn(GameObject currentUnit)
    {
        yield return new WaitForSeconds(0.3f);

        playerUnit = GameObject.FindGameObjectWithTag("PlayerUnit");
        companionUnit = GameObject.FindGameObjectWithTag("CompanionUnit");

        // Enemy prefers to attack the unit in front (the one attacking):
        float targetPickRandVal = Random.value;

        // act
        if (!playerUnit)
        {
            currentUnit.GetComponent<PlayerScript>().UseAttack(companionUnit, 0);
        }
        else if (!companionUnit)
        {
            currentUnit.GetComponent<PlayerScript>().UseAttack(playerUnit, 0);
        }
        else if (playerUnit.GetComponent<PlayerScript>().inFront)
        {
            if (targetPickRandVal <= 0.75)
                currentUnit.GetComponent<PlayerScript>().UseAttack(playerUnit, 0);
            else
                currentUnit.GetComponent<PlayerScript>().UseAttack(companionUnit, 0);
        }
        else if (companionUnit.GetComponent<PlayerScript>().inFront)
        {
            if (targetPickRandVal <= 0.75)
                currentUnit.GetComponent<PlayerScript>().UseAttack(companionUnit, 0);
            else
                currentUnit.GetComponent<PlayerScript>().UseAttack(playerUnit, 0);
        }
    }

    public void UpdateHPHUD(GameObject units)
    {
        GameObject unit;   

        foreach (Transform unitTransform in units.transform)
        {
            unit = unitTransform.gameObject;
            // Update the HUD:
            // TODO: IS IT A GOOD IDEA TO LOOK FOR THE CHARACTERS BY NAME? (IT DOES WORK)
            if (unit.name == "Player")
                playerHP.text = unit.GetComponent<PlayerScript>().HP.ToString();
            else if (unit.name == "Companion")
                companionHP.text = unit.GetComponent<PlayerScript>().HP.ToString();
            else if (unit.name == "Boss")
                bossHP.text = unit.GetComponent<PlayerScript>().HP.ToString();
        }
    }

    private void UpdateMPHUD(GameObject units)
    {
        playerMP.text = units.GetComponent<PartyStats>().partyMP.ToString();
    }

    private void CheckBattleEnded()
    {
        GameObject[] remainingEnemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");

        bool didLose = CheckIfLost();

        if (didLose)
        {
            // Lost Battle
            StartCoroutine(WaitForLoseBattle());
        }

        else if (remainingEnemyUnits.Length == 0)
        {
            // Won Battle
            StartCoroutine(WaitForWinBattle());
        }
    }

    private bool CheckIfLost()
    {
        bool didLose = true;

        foreach (Transform playerUnit in playerParty.transform)
        {
            if (playerUnit.gameObject.tag != "DeadUnit")
            {
                didLose = false;
            }
        }
        return didLose;
    }

    private void AddPartyEXP()
    {
        expGained = enemyUnits.GetComponent<PartyStats>().partyEXP;

        foreach (Transform playerUnit in playerParty.transform)
        {
            playerUnit.gameObject.GetComponent<PlayerScript>().AddExp(expGained);
        }
    }

    public void PlayTurn(GameObject currentUnit)
    {
        playerParty.GetComponent<SelectUnit>().SelectCurrentUnit(currentUnit);
        
        // Handle flip btn state once the appropriate action menus have been turned on:
        if (shouldTurnOffFlips == 1) // 1 means turn the active ones off
        {
            GameObject[] flipBtnsLocal = GameObject.FindGameObjectsWithTag("Flip");
            foreach (GameObject btn in flipBtnsLocal)
            {
                btn.gameObject.SetActive(false);
            }
            shouldTurnOffFlips = 0;
        }
        else if (shouldTurnOffFlips == -1) // -1 means turn the other unit's btn on (after this unit was revived)
        {
            if (currentUnit.name == "Player" && !currentUnit.GetComponent<PlayerScript>().inFront)
            {
                // turn on flip btn for first acting unit after his turn
                flipBtnCompanion.gameObject.SetActive(true);
            }
            else if (currentUnit.name == "Companion" && !currentUnit.GetComponent<PlayerScript>().inFront)
            {
                // turn on flip btn for first acting unit after his turn
                flipBtnPlayer.gameObject.SetActive(true);
            }
            shouldTurnOffFlips = 0;
        }
    }

    public void NextTurn()
    {
        UpdateMPHUD(playerParty);
        UpdateHPHUD(playerParty);
        UpdateHPHUD(enemyUnits);

        if (!batttleEnded)
        {
            CheckBattleEnded();
            PlayerScript currUnitScript = fighters[0];
            fighters.Remove(currUnitScript);
            GameObject currentUnit = currUnitScript.gameObject;
            fighters.Add(currUnitScript);

            if (currentUnit.tag != "DeadUnit")
            {
                UpdateMPHUD(playerParty);
                UpdateHPHUD(playerParty);

                // Check who's turn is it:
                if (currentUnit.tag == "PlayerUnit")
                {
                    // Managing effect turns:
                    for (int j = activeEffects.Count - 1; j >= 0; j--)
                    {
                        if (activeEffects[j].Duration <= 0)
                        {
                            activeEffects[j].StopEffect();
                            activeEffects.RemoveAt(j);
                        }
                        else
                        {
                            activeEffects[j].Duration--;
                        }
                    }
                    // act
                    PlayTurn(currentUnit.gameObject);
                }

                else if (currentUnit.tag == "CompanionUnit")
                {
                    PlayTurn(currentUnit.gameObject);
                }

                else
                {
                    StartCoroutine(WaitForMonsterTurn(currentUnit));
                }
            }

            else
                this.NextTurn();
        }
    }
}
