using UnityEngine;


public abstract class Effect : ScriptableObject
{
    [SerializeField]
    protected string _name = "Name";
    public string Name { get { return _name; } }

    [SerializeField]
    protected float _value = 0;
    public float Value { get { return this._value; } }

    [SerializeField]
    protected int _defaultDuration = 0;
    public int DefaultDuration { get { return this._defaultDuration; } set { _defaultDuration = value; } }

    [SerializeField]
    protected int _duration = 0;
    public int Duration { get { return this._duration; } set { _duration = value; } }

    [SerializeField]
    protected GameObject _playerParty;
    public GameObject PlayerParty { get { return this._playerParty; } }

    public abstract void DoAction();

    public abstract void UpdateDuration();

    public abstract void StopEffect();
}

