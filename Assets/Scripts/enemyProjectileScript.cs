﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectileScript : MonoBehaviour
{
    float speed;
    float damage;
    public Vector2 direction;
    public Vector2 position;
    public GameObject manager;
    public gameManagerScript mScript;
    // Start is called before the first frame update
    void Start()
    {
        mScript = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
        speed = 0.01f;
        if (mScript.waveNumber < 10)
        {
            damage = 1.0f;
        }
        else
        {
            damage = (mScript.waveNumber / 10)+1;
        }
        
        Debug.Log(damage);
    }

    // Update is called once per frame
    void Update()
    {
        go();
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //when projectile collides with an enemy, call the reducehealth method within the enemy's script
        if (collision.gameObject.tag == "Core")
        {
            collision.gameObject.SendMessage("reduceHealth", damage);
            Destroy(gameObject);
        }
    }
    //Handle movement
    void go()
    {
        position = transform.position;
        position += direction * speed;
        transform.position = position;

        if (position.x > 3 || position.x < -3 || position.y > 2 || position.y < -2)
            Destroy(gameObject);
    }
}
