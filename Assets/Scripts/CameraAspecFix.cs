using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspecFix : MonoBehaviour {
    float height, width;
    public Vector2 worldBlockSize;
    // Use this for initialization
    void Start () {
        setSizeParams();
        fixCameraSize();
        //print("H:" + height + " W:" + width);
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
}
