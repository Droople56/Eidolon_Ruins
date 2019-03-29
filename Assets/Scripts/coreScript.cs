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
        //add this to the list of cores
        manager = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
        manager.cores.Add(this.gameObject);

        health = 20;
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
