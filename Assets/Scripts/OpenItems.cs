using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenItems : MonoBehaviour {

    GameObject[] attackMenus;
    GameObject[] playTurnIcons;

    private void Start()
    {
        attackMenus = GameObject.FindGameObjectsWithTag("AttackOptions");
        playTurnIcons = GameObject.FindGameObjectsWithTag("PlayTurn");
    }

    private void OnMouseDown()
    {
        TurnOffAttacksMenu();
        TurnOffIcons();
        TurnOnItemsOptions();
    }

    private void TurnOffAttacksMenu()
    {
        foreach (GameObject attackMenu in attackMenus)
        {
            foreach (Transform attackOptBtn in attackMenu.transform)
            {
                attackOptBtn.gameObject.SetActive(false);
            }
        }
    }

    private void TurnOnItemsOptions()
    {
        foreach (Transform itemOpt in this.gameObject.transform)
        {
            itemOpt.gameObject.SetActive(true);
        }
    }

    private void TurnOffIcons()
    {
        foreach (GameObject icon in playTurnIcons)
        {
            icon.gameObject.SetActive(false);
        }
    }
}
