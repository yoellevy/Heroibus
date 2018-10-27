using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Character", order = 1)]
public class Character : ScriptableObject
{
    [SerializeField]
    private string _name; // this is the chosen name of a character (can be changed with real cash payment?)
    public string Name { get { return _name; } }

    [SerializeField]
    private int _exp; // this is the character's exp (or the exp gained by defeating an enemy character)
    public int Exp { get { return _exp; } set { _exp = value < 0 ? 0 : value; } }

    [SerializeField]
    private float _hp; // this is the current hp of the character - reduced when taking dmg in battle, drops to a minimum of 0 (character dies)
    public float HP { get { return _hp; } set { _hp = value < 0 ? 0 : value; } }

    [SerializeField]
    public float MaxHP; // this is the current max hp of the character (grows via leveling up)

    [SerializeField]
    private float _mp; // mana-points are used to perform non-trivial attacks in battle
    public float MP { get { return _mp; } set { _mp = value < 0 ? 0 : value; } }

    [SerializeField]
    public float MaxMP; // this is the current max mp of the character (grows via leveling up)

    [SerializeField]
    public float Strength; // this attribute affects the amount of pure damange added to a given weapon(example: 20str adds 2 damage to any dealt damage). 

    [SerializeField]
    public float Def; // this attribute affect the (lowered) amount of damage characters take while successfully defending.

    [SerializeField]
    public float Speed; // this attribute affects your place in the battle queue as well as how likely you are to dodge attacks (starts at 10 similar to D&D's defence) 

    [SerializeField]
    public float DefaultSpeed; // this is the current default speed of the character (grows via leveling up)

    [SerializeField]
    public Weapon Weapon;

    [SerializeField]
    public List<Attack> Attacks; // this is the list of attacks a character can use (new attacks are gained via leveling up) - an attack can change the range of dmg of a weapon as well as add strategic effects in battle.

    [SerializeField]
    public List<Potion> Potions;
}
