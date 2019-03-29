using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    public List<GameObject> cores;
    public float test;

    public List<GameObject> enemyList;

    // Use this for initialization
    void Awake()
    {
        //cores = new List<GameObject>();
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //method to spawn enemies on outer ring on random tiles (outer ring is ((-3,-2) (-3,2) (3,-2) (3,2)) only spawn on tenths aka (-3,.6) or (.3,2)

}
