using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class companionScript : MonoBehaviour
{
    private readonly float EPSILON = 0.01f;
    [SerializeField]
    public Transform followObject;

    [SerializeField]
    float speed, closeRadios;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody2D;
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followObject == null)
            return;

        Vector3 dir = followObject.position - transform.position;
        if (dir.magnitude > closeRadios)
        {
            Vector2 newDir = new Vector2(Mathf.Lerp(m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.x + dir.x, 0.8f),
                Mathf.Lerp(m_rigidbody2D.velocity.y, m_rigidbody2D.velocity.y + dir.y, 0.8f));
            m_rigidbody2D.velocity = newDir.normalized * (newDir.magnitude / closeRadios) * speed;
            m_animator.SetBool("wings", true);
            m_animator.SetBool("idle", false);
        }
        else if (m_rigidbody2D.velocity.magnitude < EPSILON)
        {
            m_animator.SetBool("wings", false);
            m_animator.SetBool("idle", true);
        }
        
        transform.localScale = new Vector3(dir.x >= 0 ? -1 : 1, 1, 1);
    }
    bool isSpeak = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isSpeak && collision.name == "CircleCollider" && ActionButtonController.intance.ActionButton)
        {//set animation?    
            GameManager.instance.player.GetComponent<PlayerRaomingScript>().followingCopanion = transform;
            GameManager.instance.player.GetComponent<PlayerRaomingScript>().pause = true;
            followObject = GameManager.instance.player.transform;
            Instantiate(Resources.Load("SpeakBox"));
            isSpeak = true;
        }
    }
}
