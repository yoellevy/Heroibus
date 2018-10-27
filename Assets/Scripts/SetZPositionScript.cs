using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZPositionScript : MonoBehaviour {

    [SerializeField]
    bool killAfter = true;
	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        if(killAfter)
            Destroy(this,0.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
