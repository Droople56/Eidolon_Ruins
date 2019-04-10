using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public gameManagerScript mScript;
    Vector2 targetPosition;
    Vector2 distanceFromCores;
    Vector2 distanceFromDoors;
    Vector2 position;

    int direction;
    bool hasMovedVertical;
    bool hasMovedHorizontal;
    public int health;
    bool hasEnteredMansion;
    int spawnDirection;
    public GameObject projectile;
    public List<GameObject> projectiles;
    public GameObject targetCore;

    Vector2 projectileTargetPosition;

    public int attackRate;
    int makeAttack;

    public int score = 10;

    public Sprite up;
    public Sprite left;
    public Sprite down;
    public Sprite right;
    // Use this for initialization
    void Start()
    {
        mScript = GameObject.Find("gameManager").GetComponent<gameManagerScript>();

        //add to a list of enemies
        //mScript.enemyList.Add(this.gameObject);

        targetPosition = Vector2.zero;
        distanceFromCores = new Vector2(0, 1000);
        distanceFromDoors = new Vector2(0, 1000);
        position = transform.position;

        direction = -1;
        //calculateTargetCore();
        hasMovedVertical = false;
        hasMovedHorizontal = false;
        health = 8;
        hasEnteredMansion = false;

        attackRate = 120;
        calculateDoorway();

    }

    private void changeSprite()
    {
        if (direction == 0)
            gameObject.GetComponent<SpriteRenderer>().sprite = up;
        else if (direction == 1)
            gameObject.GetComponent<SpriteRenderer>().sprite = left;
        else if (direction == 2)
            gameObject.GetComponent<SpriteRenderer>().sprite = down;
        else if (direction == 3)
            gameObject.GetComponent<SpriteRenderer>().sprite = right;

    }

    void calculateDoorway()
    {
        targetPosition = Vector2.zero;
        distanceFromDoors = new Vector2(0, 1000);

        foreach (GameObject d in mScript.GetComponent<gameManagerScript>().doorways)
        {
            if (d != null)
            {
                if ((transform.position - d.transform.position).magnitude < distanceFromDoors.magnitude)
                {

                    distanceFromDoors = transform.position - d.transform.position;
                    targetPosition = d.transform.position;
                }
            }

        }
    }

    void calculateTargetCore()
    {
        targetPosition = Vector2.zero;
        distanceFromCores = new Vector2(100, 100);

        foreach (GameObject t in mScript.GetComponent<gameManagerScript>().cores)
        {
            if (t != null)
            {
                if ((transform.position - t.transform.position).magnitude < distanceFromCores.magnitude)
                {

                    distanceFromCores = transform.position - t.transform.position;
                    targetPosition = t.transform.position;
                    projectileTargetPosition = t.transform.position;
                    targetCore = t;
                }
            }

        }

        if (targetPosition.y > position.y)
            targetPosition.y -= .1f;
        if (targetPosition.x < position.x)
            targetPosition.x += .1f;
        if (targetPosition.y < position.y)
            targetPosition.y += .1f;
        if (targetPosition.x > position.x)
            targetPosition.x -= .1f;
        


        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        //resets rotation after every movement cuz for some reason its rotating them 
        transform.rotation = Quaternion.identity;
        changeSprite();
        transform.position = position;

        if (hasMovedHorizontal == true && hasMovedVertical == true && targetCore != null)
        {
            MakeAttack();
        }

        if (targetCore == null && hasEnteredMansion)
        {

            calculateTargetCore();
            hasMovedHorizontal = false;
            hasMovedVertical = false;
        }

    }

    void movement()
    {

        if (!hasMovedVertical)
        {
            if (targetPosition.y > position.y)
                direction = 0;
            else if (targetPosition.y < position.y)
                direction = 2;
        }
        else if (!hasMovedHorizontal)
        {
            if (targetPosition.x < position.x)
                direction = 1;
            else if (targetPosition.x > position.x)
                direction = 3;
        }


        if (!hasMovedVertical || !hasMovedHorizontal)
        {
            if (direction == 0)
                position.y += .005f;
            if (direction == 1)
                position.x -= .005f;
            if (direction == 2)
                position.y -= .005f;
            if (direction == 3)
                position.x += .005f;

            if (Mathf.Abs((targetPosition.y - position.y)) <= .01f|| Mathf.Abs((targetPosition.y - position.y)) <= -.01f)
            {
                position.y = position.y * 10;
                position.y = Mathf.Round(position.y);
                position.y = position.y / 10;
                hasMovedVertical = true;
                spawnDirection = 0;
                
            }
            if (Mathf.Abs((targetPosition.x - position.x)) <= .01f || Mathf.Abs((targetPosition.x - position.x)) <= -.01f)
            {
                position.x = position.x * 10;
                position.x = Mathf.Round(position.x);
                position.x = position.x / 10;
                hasMovedHorizontal = true;
                spawnDirection = 1;
            }
            if (position == targetPosition)
                hasEnteredMansion = true;
        }
    }

    //method reduces enemy health when hit by projectile
    void reduceHealth(int dmg)
    {
        health-=dmg;
        if (health <= 0)
        {
            destroyEnemy();
        }
    }

    //method to destroy enemy and call methods in manager to add score/money
    void destroyEnemy()
    {
        //remove from enemy list
        mScript.enemyList.Remove(gameObject);
        //destroy enemy
        Destroy(gameObject);
        //call manager to reduce enemy count for wave
        mScript.enemiesRemaining--;

        //call manager to add money to player's bank etc
        gameManagerScript manager = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
        manager.score += score;
        manager.scoreText.text = "Score: " + manager.score;
        
    }

    //method to attack when reached target tower
    public void attack()
    {
        GameObject newProjectile = Instantiate(projectile,this.position, Quaternion.identity);

        newProjectile.GetComponent<enemyProjectileScript>().position = this.position;

        Vector2 dir = projectileTargetPosition - this.position;
        dir.Normalize();

        newProjectile.GetComponent<enemyProjectileScript>().direction = dir;
    }

    public void MakeAttack()
    {
        makeAttack++;
        if (makeAttack > attackRate)
        {
            attack();
            makeAttack = 0;
        }
    }
}
