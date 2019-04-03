using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreScript : MonoBehaviour
{
    // Start is called before the first frame update
    gameManagerScript manager;
    public int health;

    void Start()
    {


        health = 15;
    }

    // Update is called once per frame
    void Update()
    {
        die();
    }

    //method reduces enemy health when hit by projectile
    void reduceHealth(int dmg)
    {
        health -= dmg;
    }

    void die()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
