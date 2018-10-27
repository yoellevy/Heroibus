using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowPlayerScript : MonoBehaviour
{
    public Transform player;
    public Vector2 worldBlockSize;

    // camera width and height
    float height, width;

    // Use this for initialization
    void Start()
    {

        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
