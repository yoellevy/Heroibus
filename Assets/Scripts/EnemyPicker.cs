using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPicker : MonoBehaviour {

    private void OnMouseDown()
    {
        GameObject playerParty = GameObject.Find("PlayerParty");
        playerParty.GetComponent<SelectUnit>().SetAttackTarget(gameObject);
    }
}
