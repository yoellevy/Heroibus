using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "CriticalEffect", menuName = "Effect/CriticalEffect", order = 1)]
public class CriticalEffect : Effect
{
    public override void DoAction()
    {
        UpdateDuration();

        PlayerScript unitScript = null; 
        foreach (Transform unit in PlayerParty.transform)
        {
            unitScript = unit.gameObject.GetComponent<PlayerScript>();
            foreach (Attack atk in unitScript.Attacks)
            {
                if (atk.IsPureEffect)
                {
                    continue;
                }
                else if (atk.CriticalEnhancementProb + Value <= 1)
                {
                    atk.CriticalEnhancementProb += Value;
                }
                else
                {
                    atk.CriticalEnhancementProb = 1;
                }
            }
        }
        PlayerScript hero = PlayerParty.transform.GetChild(0).GetComponent<PlayerScript>();
        hero.UpdateInfoHUD(hero.gameObject, "Critical Rate Up!", 4.7f, 2.0f);
    }

    public override void UpdateDuration()
    {
        Duration = DefaultDuration;
    }

    public override void StopEffect()
    {
        foreach (Transform unit in PlayerParty.transform)
        {
            PlayerScript unitScript = unit.gameObject.GetComponent<PlayerScript>();
            foreach (Attack atk in unitScript.Attacks)
            {
                if (atk.IsPureEffect)
                {
                    continue;
                }
                atk.CriticalEnhancementProb -= Value;
            }
        }
    }
}