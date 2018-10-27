using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunFromBattle : MonoBehaviour
{

    private float runnningChance;
    private GameObject playerParty;
    private GameObject enemyParty;

    private void Start()
    {
        playerParty = GameObject.FindGameObjectWithTag("PlayerParty");
        enemyParty = GameObject.FindGameObjectWithTag("EnemyUnits");
        this.runnningChance = CalculateRunningChance();
    }

    private void OnMouseDown()
    {
        TryRunning();
    }

    private float CalculateRunningChance()
    {
        float speedDiff = playerParty.GetComponent<PartyStats>().partySpeed - enemyParty.GetComponent<PartyStats>().partySpeed;
        if (speedDiff < 0)
            speedDiff = 0;
        else if (speedDiff > 10)
            speedDiff = 10;
        return speedDiff / 10;
    }

    public void TryRunning()
    {
        float randomNumber = Random.value;
        if (randomNumber < this.runnningChance)
        {
            SceneManager.LoadScene("WorldMap");
        }
        else
        {
            GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>().NextTurn();
        }
    }
}
