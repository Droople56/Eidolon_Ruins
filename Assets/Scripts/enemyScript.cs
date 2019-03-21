using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public gameManagerScript mScript;
    Vector2 targetPosition;
    Vector2 distanceFromTowers;
    Vector2 position;
    bool isWalking;
    int direction;
    bool hasMovedVertical;
    bool hasMovedHorizontal;
    // Use this for initialization
    void Start()
    {
        targetPosition = Vector2.zero;
        distanceFromTowers = new Vector2(0, 1000);
        position = transform.position;
        isWalking = false;
        direction = -1;
        calculateTargetTower();
        hasMovedVertical = false;
        hasMovedHorizontal = false;
    }

    void calculateTargetTower()
    {
        foreach (var t in mScript.GetComponent<gameManagerScript>().towers)
        {
            if ((transform.position - t.transform.position).magnitude < distanceFromTowers.magnitude)
            {
                distanceFromTowers = transform.position - t.transform.position;
                targetPosition = t.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        transform.position = position;
        Debug.Log("X: " + transform.position.x + " Y: " + transform.position.y);
    }

    void movement()
    {


        if ((Mathf.Abs((targetPosition.y - position.y)) > .11f || Mathf.Abs((targetPosition.y - position.y)) < -.11f))
        {
            hasMovedVertical = false;
        }
        else if ((Mathf.Abs((targetPosition.x - position.x)) > .11f || Mathf.Abs((targetPosition.x - position.x)) < -.11f))
        {
            hasMovedHorizontal = false;
        }
        else
        {
            hasMovedVertical = true;
            hasMovedHorizontal = true;
        }

        if (targetPosition.y > position.y)
            direction = 0;
        else if (targetPosition.x < position.x)
            direction = 1;
        else if (targetPosition.y < position.y)
            direction = 2;
        else if (targetPosition.x > position.x)
            direction = 3;

        //Debug.Log(direction);

        if (!hasMovedVertical || !hasMovedHorizontal)
        {
            if (direction == 0)
                position.y += .01f;
            if (direction == 1)
                position.x -= .01f;
            if (direction == 2)
                position.y -= .01f;
            if (direction == 3)
                position.x += .01f;


            //Debug.Log(Mathf.Abs((targetPosition.y - position.y)));
            if (Mathf.Abs((targetPosition.y - position.y)) <= .01f)
            {
                position.y = position.y * 10;
                position.y = Mathf.Round(position.y);
                position.y = position.y / 10;
                isWalking = false;
                hasMovedVertical = true;
            }
            if (Mathf.Abs((targetPosition.x - position.x)) <= .01f)
            {
                position.x = position.x * 10;
                position.x = Mathf.Round(position.x);
                position.x = position.x / 10;
                isWalking = false;
                hasMovedHorizontal = true;
            }
        }
        else
        {
            position.y = position.y * 10;
            position.y = Mathf.Round(position.y);
            position.y = position.y / 10;

            position.x = position.x * 10;
            position.x = Mathf.Round(position.x);
            position.x = position.x / 10;
        }
    }
}
