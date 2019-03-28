using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerProjectileScript : MonoBehaviour
{
    gameManagerScript manager;


    towerScript parentTower;
    Vector3 movementVector = Vector3.zero;

    public towerScript ParentTower{
        get { return this.parentTower; }
        set { this.parentTower = value; }
    }

    int damage;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        //inherits variables from the tower
        movementVector = parentTower.dVector;
        damage = parentTower.damage;
        speed = parentTower.speed;

        manager = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * movementVector;
    }


    void CheckEnemyCollisions()
    {
        Bounds pBounds = GetComponent<SpriteRenderer>().bounds;

        foreach(GameObject enemy in manager.enemyList)
        {
            Bounds eBounds = enemy.GetComponent<SpriteRenderer>().bounds;
            if (pBounds.Intersects(eBounds))
            {
                enemy.GetComponent<enemyScript>().health -= damage;
                Debug.Log("hitski");
            }
        }
    }
}
