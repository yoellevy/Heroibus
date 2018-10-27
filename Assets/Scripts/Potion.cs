using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Potion/BasicPotion", order = 1)]
public class Potion : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name { get { return this.name; } }

    [SerializeField]
    private int _mpBoost;
    public int MPBoost { get { return this._mpBoost; } set { _mpBoost = value; } }

    [SerializeField]
    private int _hpBoost;
    public int HPBoost { get { return this._hpBoost; } set { _hpBoost = value; } }

    // private string type; //fire is strong against grass for example
    // private Effect effect;
}