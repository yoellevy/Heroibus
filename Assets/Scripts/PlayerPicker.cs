using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicker : MonoBehaviour {

    GameObject[] potionMenus;
    GameObject playerParty;

    private void OnMouseDown()
    {
        playerParty = GameObject.Find("PlayerParty");
        PlayerScript thisUnit = this.gameObject.GetComponent<PlayerScript>();
        string gainedHP = thisUnit.UsePotion(playerParty.GetComponent<SelectUnit>().GetCurrentUnit().GetComponent<PlayerScript>());

        potionMenus = GameObject.FindGameObjectsWithTag("ItemsOptions");

        TurnOffPlayerColliders();

        thisUnit.UpdateInfoHUD(this.gameObject, gainedHP, 4.0f, 0.0f);
        GameObject turnSystem = GameObject.Find("TurnBasedSystem");
        turnSystem.GetComponent<TurnBasedSystem>().NextTurn();
    }

    private void TurnOffPlayerColliders()
    {
        foreach (Transform unit in playerParty.transform)
        {
            Collider2D unitCollider = unit.gameObject.GetComponent<PlayerScript>().GetComponent<Collider2D>();
            unitCollider.enabled = false;
        }
    }
}
