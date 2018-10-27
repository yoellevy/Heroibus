using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiateAttacks : MonoBehaviour {

    // THIS USED TO BE ATTACHED TO TURNBASEDSYSTEM AND WOULD INITIATE THE PLAYER'S AND COMPANION'S ATTACKS AS BUTTONS

    [SerializeField]
    private GameObject actionsMenu, actionsMenuCompanion;

    // Use this for initialization
    void Start () {

        PlayerScript player = GameObject.FindGameObjectWithTag("PlayerUnit").GetComponent<PlayerScript>();
        PlayerScript companion = GameObject.FindGameObjectWithTag("CompanionUnit").GetComponent<PlayerScript>();

        List<Attack> playerAttacks = player.Attacks;
        List<Attack> companionAttacks = companion.Attacks;

        print("PLAYER NUM ATTACKS IS: " + playerAttacks.Count);
        print("COMPANION NUM ATTACKS IS: " + companionAttacks.Count);

        for (int i = 0; i < playerAttacks.Count; i++)
        {
            print("INITIATING THIS ATTACK : " + playerAttacks[i].name);
            GameObject btn = (GameObject)Instantiate(Resources.Load(string.Format("Atk{0}Button", i+1)));
            //TODO: CHECK HOW THIS 'false' argument maintains the button scale and position (FINALLY!!):
            btn.transform.SetParent(actionsMenu.transform, false);
            btn.GetComponentInChildren<Text>().text = playerAttacks[i].name;
        }

        for (int i = 0; i < companionAttacks.Count; i++)
        {
            GameObject btn = (GameObject)Instantiate(Resources.Load(string.Format("Atk{0}Button", i + 1)));
            //TODO: CHECK HOW THIS 'false' argument maintains the button scale and position (FINALLY!!):
            btn.transform.SetParent(actionsMenuCompanion.transform, false);
            btn.GetComponentInChildren<Text>().text = companionAttacks[i].name;
        }
    }
}
