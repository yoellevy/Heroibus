using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonController : MonoBehaviour
{
    [SerializeField]
    float timeActive = 1; // time the button can be active on click
    public static ActionButtonController intance;

    private int fingerId;
    private float timeButtonStart;

    public bool ActionButton { get; private set; }

    // Use this for initialization
    void Start()
    {
        if (intance == null)
        { intance = this; }
        else { Destroy(gameObject); }

        fingerId = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
            foreach (Touch touch in Input.touches)
            {
                if (fingerId == -1)
                {
                    if (touch.phase == TouchPhase.Began &&
                                        touch.position.x > Screen.width / 2)
                    {
                        fingerId = touch.fingerId;
                        ActionButton = true;
                    }
                }
                else if (fingerId == touch.fingerId)
                    if (Time.time - timeButtonStart > timeActive || touch.phase == TouchPhase.Ended)
                    {
                        fingerId = -1;
                        ActionButton = false;
                    } 

            }
    }
}
