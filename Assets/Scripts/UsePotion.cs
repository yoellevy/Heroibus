using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePotion : MonoBehaviour {

    [SerializeField]
    private int index;

    GameObject playerParty;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        playerParty = GameObject.Find("PlayerParty");
        playerParty.GetComponent<SelectUnit>().SelectPotion(this.index);

        TurnOnPlayerColliders();

        // One of each potion so once used, the potion is destroyed:
        Destroy(this.gameObject);
    }

    private void TurnOnPlayerColliders()
    {
        foreach (Transform unit in playerParty.transform)
        {
            Collider2D unitCollider = unit.gameObject.GetComponent<PlayerScript>().GetComponent<Collider2D>();
            unitCollider.enabled = true;
        }
    }
}
