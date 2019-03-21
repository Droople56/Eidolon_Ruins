using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Use this for initialization
    Vector2 position;


    bool isWalking;
    bool isWPressed;
    bool isAPressed;
    bool isSPressed;
    bool isDPressed;
    int direction;
    Vector2 targetPosition;

    void Start()
    {
        position = this.transform.position;
        isWalking = false;
        targetPosition = position;
        isWPressed = false;
        isAPressed = false;
        isSPressed = false;
        isDPressed = false;
        direction = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //movement();
        movement();
        transform.position = position;
        //Debug.Log("Tranform X: " + transform.position.x+ " Tranform Y: " + transform.position.y);
    }

    void movement()
    {
        //check input
        if (Input.GetKey(KeyCode.W) && !isAPressed && !isSPressed && !isDPressed && !isWPressed)
        {
            isWPressed = true;
            isWalking = true;
            direction = 0;
        }
        if (Input.GetKey(KeyCode.A) && !isWPressed && !isSPressed && !isDPressed && !isAPressed)
        {
            isAPressed = true;
            isWalking = true;
            direction = 1;
        }
        if (Input.GetKey(KeyCode.S) && !isWPressed && !isAPressed && !isDPressed && !isSPressed)
        {
            isSPressed = true;
            isWalking = true;
            direction = 2;

        }
        if (Input.GetKey(KeyCode.D) && !isWPressed && !isAPressed && !isSPressed && !isDPressed)
        {
            isDPressed = true;
            isWalking = true;
            direction = 3;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            isWalking = false;

        if (isWalking)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isWPressed)
                    position.y += .015f;
                if (isAPressed)
                    position.x -= .015f;
                if (isSPressed)
                    position.y -= .015f;
                if (isDPressed)
                    position.x += .015f;
            }
            else
            {
                if (isWPressed)
                    position.y += .01f;
                if (isAPressed)
                    position.x -= .01f;
                if (isSPressed)
                    position.y -= .01f;
                if (isDPressed)
                    position.x += .01f;
            }
            //moves player the direction they move

        }
        else
        {
            Vector2 tempPosition = Vector2.zero;
            if (direction == 0)
            {
                //sets target position to next closest position in the direction the player was moving
                tempPosition.y = position.y * 10;
                tempPosition.y = Mathf.CeilToInt(tempPosition.y);
                tempPosition.y /= 10;

                targetPosition.y = tempPosition.y;
            }
            if (direction == 1)
            {
                tempPosition.x = position.x * 10;
                tempPosition.x = Mathf.FloorToInt(tempPosition.x);
                tempPosition.x /= 10;

                targetPosition.x = tempPosition.x;
            }
            if (direction == 2)
            {
                tempPosition.y = position.y * 10;
                tempPosition.y = Mathf.FloorToInt(tempPosition.y);
                tempPosition.y /= 10;

                targetPosition.y = tempPosition.y;
            }
            if (direction == 3)
            {
                tempPosition.x = position.x * 10;
                tempPosition.x = Mathf.CeilToInt(tempPosition.x);
                tempPosition.x /= 10;

                targetPosition.x = tempPosition.x;
            }

            if (Mathf.Abs(targetPosition.y) - Mathf.Abs(position.y) >= .01f || Mathf.Abs(targetPosition.y) - Mathf.Abs(position.y) <= -.01f || Mathf.Abs(targetPosition.x) - Mathf.Abs(position.x) >= .01f || Mathf.Abs(targetPosition.x) - Mathf.Abs(position.x) <= -.01f)
                position = Vector2.MoveTowards(position, targetPosition, .01f);
            else
            {
                //allows the user to press a button again
                isWalking = false;
                isWPressed = false;
                isAPressed = false;
                isSPressed = false;
                isDPressed = false;

                //rounds position to a clean
                position.x = position.x * 10;
                position.y = position.y * 10;
                position.x = Mathf.Round(position.x);
                position.y = Mathf.Round(position.y);
                position.x = position.x / 10;
                position.y = position.y / 10;
            }
        }
    }
}
