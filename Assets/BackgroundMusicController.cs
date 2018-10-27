using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour {

    [SerializeField]
    List<AudioClip> audioClips;

    public static BackgroundMusicController instance;
    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayClip(int idx)
    {        
        GetComponent<AudioSource>().clip = audioClips[idx];
        GetComponent<AudioSource>().Play();
    }
}
