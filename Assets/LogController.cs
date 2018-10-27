using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    bool animationStart = false;
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (animationStart)
            return;
        if (collision.name == "CircleCollider" && ActionButtonController.intance.ActionButton)
        {
            animationStart = GameManager.instance.StartBossBattle();
        }
    }

}
