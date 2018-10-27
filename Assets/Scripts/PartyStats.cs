using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyStats : MonoBehaviour
{
    public float partySpeed = 0;
    public float partyMaxMP = 0;
    public float partyMP = 0;
    public int partyEXP = 0;

    private void Start()
    {
        UpdateStats();
        partyMP = partyMaxMP;
    }

    private void UpdateStats()
    {
        partySpeed = CalculateAvgSpeed();
        partyMaxMP = CalculateTotalMP();
        partyEXP = CalculateTotalEXP();
    }

    private float CalculateAvgSpeed()
    {
        float spd = 0;
        int counter = 0;

        foreach (Transform unit in this.gameObject.transform)
        {
            counter++;
            spd += unit.gameObject.GetComponent<PlayerScript>().Speed;
        }
        return spd / counter;
    }

    private float CalculateTotalMP()
    {
        float totMP = 0;

        foreach (Transform unit in this.gameObject.transform)
        {
            totMP += unit.gameObject.GetComponent<PlayerScript>().MP;
        }
        return totMP;
    }

    private int CalculateTotalEXP()
    {
        int expGained = 0;

        foreach (Transform unit in this.gameObject.transform)
        {
            expGained += unit.gameObject.GetComponent<PlayerScript>().Exp;
        }
        return expGained;
    }
}
