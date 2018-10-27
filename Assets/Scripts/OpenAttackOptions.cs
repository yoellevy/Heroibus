using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAttackOptions : MonoBehaviour {

    GameObject[] potionMenus;
    GameObject[] playTurnIcons;

    private void OnMouseDown()
    {
        potionMenus = GameObject.FindGameObjectsWithTag("ItemsOptions");
        playTurnIcons = GameObject.FindGameObjectsWithTag("PlayTurn");
        TurnOnAttackOptions();
        TurnOffPotionsMenu();
        TurnOffIcons();
    }

    private void TurnOnAttackOptions()
    {
        foreach (Transform attackOptBtn in this.gameObject.transform)
        {
            attackOptBtn.gameObject.SetActive(true);
        }
    }

    private void TurnOffPotionsMenu()
    {
        foreach (GameObject potionMenu in potionMenus)
        {
            foreach (Transform potion in potionMenu.transform)
            {
                potion.gameObject.SetActive(false);
            }
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
