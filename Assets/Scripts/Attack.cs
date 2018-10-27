using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attack/BasicAttack", order = 1)]
public class Attack : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name { get { return this.name; } }

    [SerializeField]
    private float _upperRangeEnhancer; // multiply the weapon damage range by some ratio(enhancer) - upper bound.
    public float UpperRangeEnhancer { get { return this._upperRangeEnhancer; } set { _upperRangeEnhancer = value; } }

    [SerializeField]
    private float _lowerRangeEnhancer; // multiply the weapon damage range by some ratio(enhancer) - lower bound.
    public float LowerRangeEnhancer { get { return this._lowerRangeEnhancer; } set { _lowerRangeEnhancer = value; } }

    [SerializeField]
    private float _criticalEnhancementRatio; // multiply the weapon damage range by some ratio(enhancer) if critical hit
    public float CriticalEnhancementRatio { get { return this._criticalEnhancementRatio; } set { _criticalEnhancementRatio = value; } }

    [SerializeField]
    private float _criticalEnhancementProb; // chance of getting a critical hit
    public float CriticalEnhancementProb { get { return this._criticalEnhancementProb; } set { _criticalEnhancementProb = value; } }

    [SerializeField]
    private int _mpDemand;
    public int MpDemand { get { return this._mpDemand; } set { _mpDemand = value; } }

    [SerializeField]
    private bool _isPureEffect;
    public bool IsPureEffect { get { return this._isPureEffect; } set { _isPureEffect = value; } }

    // private string type; //fire is strong against grass for example
    [SerializeField]
    private Effect[] _effects;
    public Effect[] Effects { get { return this._effects; } set { _effects = value; } }
}

