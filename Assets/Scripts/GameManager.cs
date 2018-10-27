using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance=null;

    public Transform player;

    public Transform floatingLog;
	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            //DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        BackgroundMusicController.instance.PlayClip(1);
	}

    public bool StartBossBattle()
    {
        if(player.GetComponent<PlayerRaomingScript>().followingCopanion != null &&
            GameObject.Find("Sword") == null)
        {
            Camera.main.GetComponent<WorldMoveNextScript>().pause = true;
            StartCoroutine(startBattle());
            StartCoroutine(Camera.main.GetComponent<WorldMoveNextScript>().moveToBoss());
            return true;
        }
        return false;
    }

    IEnumerator startBattle()
    {
        //start animation and load battle scene
        floatingLog.GetComponent<Animator>().SetBool("active", true);
        player.GetComponent<PlayerRaomingScript>().startJumpToBoss();
        yield return (new WaitForSeconds(4f));
        Instantiate(Resources.Load("BattleTextBox"));               
    }
}
