using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SelectUnit : MonoBehaviour
{
    private GameObject[] enemyUnits;

    private GameObject currentUnit, currentAttackTarget;

    private GameObject actionsMenu, actionsMenuCompanion;
    //private GameObject enemyUnitsMenu;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle")
        {
            this.actionsMenu = GameObject.Find("ActionsMenu");
            this.actionsMenuCompanion = GameObject.Find("ActionsMenuCompanion");
            //this.enemyUnitsMenu = GameObject.Find("EnemyUnitsMenu");
            enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
            //SELECT DEFAULT TARGET
            currentAttackTarget = enemyUnits[0];
        }
    }

    public GameObject GetCurrentUnit()
    {
        return currentUnit;
    }

    public void SetAttackTarget(GameObject target)
    {
        currentAttackTarget = target;
    }

    public void SelectCurrentUnit(GameObject unit)
    {
        this.currentUnit = unit;

        if (currentUnit.tag == "PlayerUnit")
        {
            this.actionsMenu.SetActive(true);
            this.actionsMenuCompanion.SetActive(false);
        }
        else if (currentUnit.tag == "CompanionUnit")
        {
            this.actionsMenuCompanion.SetActive(true);
            this.actionsMenu.SetActive(false);
        }

        //this.currentUnit.GetComponent<PlayerUnitAction>().updateHUD();
    }

    public void SelectAttack(int i)
    {
        this.currentUnit.GetComponent<PlayerScript>().SetCurrAttack(i);

        ArrangeActivatedMenus();

        AttackEnemyTarget(currentAttackTarget);
    }

    public void SelectPotion(int i)
    {
        this.currentUnit.GetComponent<PlayerScript>().SetCurrPotion(i);

        ArrangeActivatedMenus();
    }

    private void ArrangeActivatedMenus()
    {
        if (currentUnit.tag == "PlayerUnit")
            this.actionsMenu.SetActive(false);
        else if (currentUnit.tag == "CompanionUnit")
            this.actionsMenuCompanion.SetActive(false);
    }

    public void AttackEnemyTarget(GameObject target)
    {
        PlayerScript currUnit = this.currentUnit.GetComponent<PlayerScript>();
        currUnit.UseAttack(target, currUnit.currAtkInd);
    }
}
