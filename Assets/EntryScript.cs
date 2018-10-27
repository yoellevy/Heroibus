using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryScript : MonoBehaviour
{
    private void Start()
    {
        if (BackgroundMusicController.instance != null)
            BackgroundMusicController.instance.PlayClip(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene("WorldMap");
        }
    }
}
