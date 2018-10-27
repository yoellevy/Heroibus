using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SpeedEffect", menuName = "Effect/SpeedEffect", order = 1)]
public class SpeedEffect : Effect
{
    public override void DoAction()
    {
        UpdateDuration();
        PlayerScript companion = PlayerParty.transform.GetChild(1).GetComponent<PlayerScript>();
        companion.Speed += Value;
        companion.UpdateInfoHUD(companion.gameObject, "Speed Up!", 4.2f, 0.0f);
    }

    public override void UpdateDuration()
    {
        Duration = DefaultDuration;
    }

    public override void StopEffect()
    {
        PlayerParty.transform.GetChild(1).GetComponent<PlayerScript>().Speed -= Value;
    }
}