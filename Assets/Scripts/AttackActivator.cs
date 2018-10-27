using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackActivator : MonoBehaviour
{

    [SerializeField]
    private int index;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        GameObject playerParty = GameObject.Find("PlayerParty");
        playerParty.GetComponent<SelectUnit>().SelectAttack(this.index);
    }

}