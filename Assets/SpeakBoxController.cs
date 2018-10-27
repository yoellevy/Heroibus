using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeakBoxController : MonoBehaviour
{

    int currentIdx = 0;
    [SerializeField]
    List<Sprite> textSprites;
    [SerializeField]
    bool killAfter = false;
    [SerializeField]
    string moveToSceneNamed = "";
    // Use this for initialization
    void Start()
    {
        currentIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (killAfter)
            {
                Destroy(gameObject);
                return;
            }

            currentIdx += 1;
            if (textSprites.Count > currentIdx)
            {
                GetComponent<SpriteRenderer>().sprite = textSprites[currentIdx];
            }
            else if(moveToSceneNamed == "")
            {
                GameManager.instance.player.GetComponent<PlayerRaomingScript>().pause = false;
                Destroy(gameObject);
            } else
            {
                SceneManager.LoadScene(moveToSceneNamed);
            }
        }
    }
}
