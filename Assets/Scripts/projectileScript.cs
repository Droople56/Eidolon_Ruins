using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    int direction; //0-up, 1-left, 2-down, 3-right
    float speed;
    float damage;
    Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        direction = -1;
        speed = 0.15f;
        damage = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        go();
        transform.position = position;
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
    }
}
