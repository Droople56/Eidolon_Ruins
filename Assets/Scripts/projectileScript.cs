using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    int direction; //0-up, 1-left, 2-down, 3-right
    float speed;
    float damage;
    Vector2 position;
    public GameObject plyr;
    // Start is called before the first frame update
    void Start()
    {
        plyr = GameObject.Find("Player");
        direction = plyr.GetComponent<playerScript>().direction;
        speed = 0.03f;
        damage = 2.0f;
        position = plyr.GetComponent<playerScript>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        go();
        transform.position = position;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        //when projectile collides with an enemy, call the reducehealth method within the enemy's script
        if (collision.gameObject.tag == "Enemy")
        {
            
            collision.gameObject.SendMessage("reduceHealth", damage);
            Destroy(gameObject);
            plyr.GetComponent<playerScript>().numOfProjectiles--;

        }
    }
    //Handle movement
    void go()
    {
        //up
        if (direction == 0)
        {
            position.y += speed;
        }
        //left
        if (direction == 1)
        {
            position.x -= speed;
        }
        //down
        if (direction == 2)
        {
            position.y -= speed;
        }
        //right
        if (direction == 3)
        {
            position.x += speed;
        }

        if (position.x > 3 || position.x < -3 || position.y > 2 || position.y < -2)
        {
            Destroy(gameObject);
            plyr.GetComponent<playerScript>().numOfProjectiles--;
        }
            
    }
}
