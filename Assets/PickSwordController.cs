using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSwordController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "CircleCollider" && ActionButtonController.intance.ActionButton)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
