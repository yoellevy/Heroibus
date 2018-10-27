using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerRaomingScript : MonoBehaviour
{
    [SerializeField]
    public Transform followingCopanion;
    public bool pause = false;
    [Serializable]
    public class playerSprite
    {
        [SerializeField]
        public Sprite head, body, rightLeg, leftLeg;
    }
    [SerializeField]
    List<playerSprite> playerSprites;
    [SerializeField]
    SpriteRenderer headRenderer, bodyRenderer, rArmRenderer, lArmRenderer, rLegRenderer, lLegRenderer;

    [SerializeField]
    int movmentQuantization = 4;
    #region public    
    public float speed = 7;

    #endregion

    #region private parameters
    private float currentSpeed = 0;
    private float angle = 0;
    private bool isSprint = false;
    private int moveFingerId = -1;
    private int actionFingerId = -1;
    private float actionDeltaTime;
    private Vector2 lastDir;
    private float lastTouchTimestamp = -1;
    #endregion

    #region private component
    Rigidbody2D m_rigidbody;
    Collider2D m_collider;
    Animator m_animator;
    #endregion
    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();
    }
    void setAnimationVar(string trueAnim)
    {
        m_animator.SetBool("WalkRight", false);
        m_animator.SetBool("WalkLeft", false);
        m_animator.SetBool("WalkFront", false);
        m_animator.SetBool("WalkBack", false);
        if (trueAnim != "idle")
            m_animator.SetBool(trueAnim == "WalkRight" ? "WalkLeft" : trueAnim, true);

        int idx = 0; // trueAnim == "idle" || trueAnim == "WalkFront"
        if (!(trueAnim == "idle" || trueAnim == "WalkFront"))
            idx = trueAnim == "WalkBack" ? 2 : 1;

        headRenderer.sprite = playerSprites[idx].head;
        bodyRenderer.sprite = playerSprites[idx].body;
        lLegRenderer.sprite = playerSprites[idx].leftLeg;
        rLegRenderer.sprite = playerSprites[idx].rightLeg;
        transform.localScale = new Vector3(trueAnim == "WalkRight" ? -1 : 1, 1, 1);

    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir3 = LeftJoystick.intance.GetInputDirection();
        Vector2 realDir = new Vector2((float)((int)(dir3.x * movmentQuantization)) / movmentQuantization,
            (float)((int)(dir3.y * movmentQuantization)) / movmentQuantization);


        m_rigidbody.velocity = realDir * speed;
        if (pause)
            m_rigidbody.velocity = Vector2.zero;
        //print(realDir);
        if ((realDir.x != 0 || realDir.y != 0) && !pause)
            if (Mathf.Abs(realDir.x) > Mathf.Abs(realDir.y))
            { // movement on x axis
                setAnimationVar(realDir.x > 0 ? "WalkRight" : "WalkLeft");
            }
            else
            {
                setAnimationVar(realDir.y > 0 ? "WalkBack" : "WalkFront");
            }
        else
        {
            setAnimationVar("idle");
        }
    }
    bool actionclick = false;

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    //if (collision.gameObject.tag == "Interactable" && actionclick)
    //    //{
    //    //    GameManager.instance.StartBossBattle();            
    //    //}
    //}

    public void startJumpToBoss()
    {
        StartCoroutine(movePlayerTolog());
        m_animator.SetBool("startBattle", true);
    }
    IEnumerator movePlayerTolog()
    {
        yield return new WaitForSeconds(2);
        transform.position = new Vector3(54.8f, 222.72f, transform.position.z);
    }
}
