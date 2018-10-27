//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using UnityEngine.SceneManagement;

//public class EnemyScript : MonoBehaviour, IComparable
//{

//    [SerializeField]
//    private Character _enemy;

//    //[SerializeField]
//    //private Character _companion;

//    // Use this for initialization
//    private static bool created = false;

//    public int nextActTurn;

//    private bool dead = false;

//    public void calculateNextActTurn(int currentTurn)
//    {
//        this.nextActTurn = currentTurn + (int)Math.Ceiling(100.0f / _enemy.Speed);
//    }

//    public int CompareTo(object otherStats)
//    {
//        return nextActTurn.CompareTo(((PlayerScript)otherStats).nextActTurn);
//    }

//    public bool isDead()
//    {
//        return this.dead;
//    }


//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown("space"))
//            print(_enemy.HP);
//        if (Input.GetKeyDown("x"))
//            _enemy.HP -= 200;
//    }
//}
