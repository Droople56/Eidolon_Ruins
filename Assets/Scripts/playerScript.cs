using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Use this for initialization
    Vector2 position;

    public Camera cam;

    bool isWalking;
    bool isWPressed;
    bool isAPressed;
    bool isSPressed;
    bool isDPressed;
    public int direction;
    public int aimDirection;
    Vector2 targetPosition;
    public GameObject projectile;
    public int numOfProjectiles;
    public Sprite up;
    public Sprite left;
    public Sprite down;
    public Sprite right;
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
        aimDirection = 0;
        numOfProjectiles = 0;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        
        movement();
        changeSprite();
        transform.position = position;
        aimAtMouse();
        attack();
    }

    private void changeSprite()
    {
        if(aimDirection == 0)
            gameObject.GetComponent<SpriteRenderer>().sprite = up;
        else if(aimDirection==1)
            gameObject.GetComponent<SpriteRenderer>().sprite = left;
        else if(aimDirection==2)
            gameObject.GetComponent<SpriteRenderer>().sprite = down;
        else if(aimDirection==3)
            gameObject.GetComponent<SpriteRenderer>().sprite = right;

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

        //if released set player to not walk
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            isWalking = false;

        //if player is walking, move them in the direction they are going
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
                    position.y += .02f;
                if (isAPressed)
                    position.x -= .02f;
                if (isSPressed)
                    position.y -= .02f;
                if (isDPressed)
                    position.x += .02f;
            }
            //moves player the direction they move

        }
        else
        {
            //if not walking, finish out the movement from the previous movement
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

            //if within .01, round position to be on the grid
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

                //rounds position to nearest tenth when finished with movement
                position.x = position.x * 10;
                position.y = position.y * 10;
                position.x = Mathf.Round(position.x);
                position.y = Mathf.Round(position.y);
                position.x = position.x / 10;
                position.y = position.y / 10;
            }
        }
    }

    //This handles the firing of projectiles in a given direction.
    void attack()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)&&numOfProjectiles<5)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            numOfProjectiles++;
        }
    }

    //aiming using the mouse cursor
    void aimAtMouse()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //May need these
        // && Math.Abs(mousePos.x) > Math.Abs(mousePos.y)
        // && Math.Abs(mousePos.y) > Math.Abs(mousePos.x)
        //up
        if (mousePos.y >= transform.position.y && Math.Abs(mousePos.y) > Math.Abs(mousePos.x))
        {
            aimDirection = 0;
        }
        //left
        else if (mousePos.x < transform.position.x && Math.Abs(mousePos.x) > Math.Abs(mousePos.y))
        {
            aimDirection = 1;
        }
        //down
        else if (mousePos.y < transform.position.y && Math.Abs(mousePos.y) > Math.Abs(mousePos.x))
        {
            aimDirection = 2;
        }
        //right
        else if (mousePos.x > transform.position.x && Math.Abs(mousePos.x) > Math.Abs(mousePos.y))
        {
            aimDirection = 3;
        }

        
    }
}
