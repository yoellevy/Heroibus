using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTurnOrder : MonoBehaviour {

    GameObject flipBtnPlayer;
    GameObject flipBtnCompanion;

    private void Awake()
    {
        flipBtnPlayer = GameObject.Find("FlipBtnP");
        flipBtnCompanion = GameObject.Find("FlipBtnC");
    }

    private void OnMouseDown()
    {
        TurnBasedSystem turnBasedSystem = GameObject.FindGameObjectWithTag("TurnBasedSystem").GetComponent<TurnBasedSystem>();
        List<PlayerScript> fighters = turnBasedSystem.fighters;

        //FLIP
        PlayerScript currentlyActing = fighters[fighters.Count - 1];

        // Handling Flip Buttons:
        if (currentlyActing.inFront)
        {
            if (currentlyActing.tag == "PlayerUnit")
            {
                flipBtnPlayer.SetActive(false);
                flipBtnCompanion.SetActive(true);
            }
            else if (currentlyActing.tag == "CompanionUnit")
            {
                flipBtnPlayer.SetActive(true);
                flipBtnCompanion.SetActive(false);
            }
            currentlyActing.inFront = false;
            fighters[0].inFront = true;
        }

        // Fliping order:
        fighters.Remove(currentlyActing);
        fighters.Insert(0, currentlyActing);
        fighters[0] = fighters[1];
        fighters[1] = currentlyActing;
        turnBasedSystem.NextTurn();
    }
}
