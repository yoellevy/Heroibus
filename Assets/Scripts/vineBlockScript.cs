using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineBlockScript : MonoBehaviour
{

    private bool toBlock = false;
    Animator m_animator;
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_animator.SetBool("block", toBlock);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CircleCollider")
        {
            toBlock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CircleCollider")
        {
            toBlock = false;
        }
    }
}
