using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDefence : MonoBehaviour {

    // THIS WAS USED TO ACTIVATE DEFENCE UPON CLICKING DEFEND

    GameObject[] attackMenus;
    PlayerScript player;
    PlayerScript companion;

    private void Start()
    {
        attackMenus = GameObject.FindGameObjectsWithTag("AttackOptions");
        player = GameObject.FindGameObjectWithTag("PlayerUnit").GetComponent<PlayerScript>();
        companion = GameObject.FindGameObjectWithTag("CompanionUnit").GetComponent<PlayerScript>();
    }

    private void OnMouseDown()
    {
        // DEACTIVATE THE SPECIFIC ATTACK BUTTONS
        
        GameObject actionsMenu = this.gameObject.transform.parent.gameObject;

        foreach (GameObject attackMenu in attackMenus) {
            {
                foreach (Transform attackOptBtn in attackMenu.transform)
                {
                    attackOptBtn.gameObject.SetActive(false);
                }
            }
        }

        //actionsMenu.SetActive(false);

        //if (actionsMenu.name == "ActionsMenu")
        //{
        //    player.PumpDef();
        //}
        //else if (actionsMenu.name == "ActionsMenuCompanion")
        //{
        //    companion.PumpDef();
        //}

        GameObject turnSystem = GameObject.Find("TurnBasedSystem");
        turnSystem.GetComponent<TurnBasedSystem>().NextTurn();
    }
}
