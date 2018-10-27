using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    [SerializeField]
    private string _name = "Name";
    public string Name { get { return _name; } }

    [SerializeField]
    private float _dmgMin;
    public float DmgMin { get { return this._dmgMin; } set { _dmgMin = value < 0 ? 0 : value; } }

    [SerializeField]
    private float _dmgMax;
    public float DmgMax { get { return this._dmgMax; } set { _dmgMax = value < 0 ? 0 : value; } }

    // Scroll slots here...
}

