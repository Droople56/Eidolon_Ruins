using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;

    void Start()
    {
        health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //die();
    }

    void die()
    {
        if (health >= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
