using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFallScript : MonoBehaviour
{

    Animator m_animator;
    [SerializeField]
    Transform swordCollider;
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    bool isFall = false;
    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (!isFall && collision.name == "CircleCollider" && ActionButtonController.intance.ActionButton)
        {
            m_animator.SetBool("isFall", true);
            Destroy(this.GetComponent<Collider2D>());
            swordCollider.gameObject.SetActive(true);
            Destroy(this);
        }
        
    }

    
}
