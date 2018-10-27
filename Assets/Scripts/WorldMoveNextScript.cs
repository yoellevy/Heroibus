using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class WorldMoveNextScript : MonoBehaviour
{
    [Range(0, 2f)]
    public float CameraMovmentTime = 0.25f;
    public int frameToMove = 30;
    public Transform player;
    public Vector2 worldBlockSize;
    Vector2 currentBlockCenter;
    public bool pause;
    // camera width and height
    float height, width;
    // Use this for initialization
    void Start()
    {
        setSizeParams();
        currentBlockCenter = transform.position;
        fixCameraSize();
    }

    void setSizeParams()
    {
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    void fixCameraSize()
    {
        if (height > worldBlockSize.y)
        {
            float newSize = Camera.main.orthographicSize * worldBlockSize.y / height;
            Camera.main.orthographicSize = newSize;
            setSizeParams();
        }
        if (width > worldBlockSize.x)
        {
            float newSize = Camera.main.orthographicSize * worldBlockSize.x / width;
            Camera.main.orthographicSize = newSize;
            setSizeParams();
        }
    }
    private bool isCameraMoving = false;
    // Update is called once per frame
    void Update()
    {
        if (pause)
            return;
        if (!isCameraMoving)
            moveBlock();
        followPlayer();
    }

    private void followPlayer()
    {
        float newX = player.position.x;
        float newY = player.position.y;

        if (Mathf.Abs(newX - currentBlockCenter.x) + (width / 2) > worldBlockSize.x / 2)
        {//not good
            newX = transform.position.x;
        }
        if (Mathf.Abs(newY - currentBlockCenter.y) + (height / 2) > worldBlockSize.y / 2)
        {//not good
            newY = transform.position.y;
        }
        Camera.main.transform.position = new Vector3(newX, newY, -10);
    }

    private void moveBlock()
    {

        Vector2 posDelta = player.transform.position - transform.position;

        // check if player in scope
        if (Mathf.Abs(posDelta.x) <= width / 2 && Mathf.Abs(posDelta.y) <= height / 2)
            return;

        int signX = posDelta.x > 0 ? 1 : -1, signY = posDelta.y > 0 ? 1 : -1; ;
        if (Mathf.Abs(posDelta.y) <= height / 2)
            signY = 0;
        else
            signX = 0;

        Vector2 pos = new Vector2(transform.position.x + signX * width, transform.position.y + signY * height);
        currentBlockCenter = new Vector2(currentBlockCenter.x + signX * worldBlockSize.x, currentBlockCenter.y + signY * worldBlockSize.y);
        isCameraMoving = true;
        StartCoroutine(moveCameraTo(pos));
    }

    IEnumerator moveCameraTo(Vector2 pos)
    {
        Transform cam = Camera.main.transform;
        Vector3 originalPos = cam.position;
        for (int i = 0; i < frameToMove; i++)
        {
            cam.position = new Vector3(cam.position.x+(pos.x-originalPos.x)/frameToMove,
                cam.position.y + (pos.y - originalPos.y) / frameToMove, cam.position.z);
            yield return new WaitForSeconds(CameraMovmentTime / frameToMove);
        }
        isCameraMoving = false;
    }

    public IEnumerator moveToBoss()
    {
        Vector2 pos = new Vector2(0, 250);
        Transform cam = Camera.main.transform;
        for (int i = 0; i < 150; i++)
        {
            cam.position = new Vector3(Mathf.Lerp(cam.position.x, pos.x, 0.03f),
            Mathf.Lerp(cam.position.y, pos.y, 0.03f), cam.position.z);
            yield return new WaitForSeconds(0.03f * CameraMovmentTime);
        }
    }
}
