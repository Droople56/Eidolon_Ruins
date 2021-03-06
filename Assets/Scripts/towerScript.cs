﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerScript : MonoBehaviour
{
    gameManagerScript manager;
    storeScript store;

    public GameObject towerProjectilePrefab;

    //can set the direction in the inspector
    public string direction;

    //direction the projectiles will go
    [HideInInspector] public Vector3 dVector;

    public float fireRate;
    float currentTime;

    public int damage = 1;
    public float speed = 0.02f;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
        manager.towerList.Add(this);

        store = GameObject.Find("gameManager").GetComponent<storeScript>();
        this.damage = store.currentTowerDamage;
        this.speed = store.currentTowerSpeed;

        if (direction == "")
        {
            direction = "right";
            dVector = new Vector3(1, 0);
        }

        currentTime = fireRate/2.0f;

        DetermineDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime >= fireRate)
        {
            FireProjectile();
            currentTime = 0.0f;
        }
        else
        {
            currentTime += Time.fixedDeltaTime;
        }
    }

    //determine the direction projectiles will go
    void DetermineDirection()
    {
        switch (direction)
        {
            case "left":
                dVector = new Vector3(-1, 0);
                break;
            case "right":
                dVector = new Vector3(1, 0);
                break;
            case "up":
                dVector = new Vector3(0, 1);
                break;
            case "down":
                dVector = new Vector3(0, -1);
                break;
            default:
                dVector = new Vector3(1, 0);
                break;
        }
    }

    void FireProjectile()
    {
        GameObject projectile = GameObject.Instantiate(towerProjectilePrefab, transform.position, Quaternion.identity, transform);
        projectile.GetComponent<towerProjectileScript>().ParentTower = this;
    }
}
