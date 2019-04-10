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
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * movementVector;
        if (transform.position.x > 3 || transform.position.x < -3 || transform.position.y > 2 || transform.position.y < -2)
            Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        //when projectile collides with an enemy, call the reducehealth method within the enemy's script
        if (collision.gameObject.tag == "Enemy")
        {
            
            collision.gameObject.SendMessage("reduceHealth", damage);
            Destroy(gameObject);
        }
    }
}
