using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public gameManagerScript mScript;
    Vector2 targetPosition;
    Vector2 distanceFromCores;
    Vector2 position;
    bool isWalking;
    int direction;
    bool hasMovedVertical;
    bool hasMovedHorizontal;
    public int health;

    public GameObject projectile;
    public List<GameObject> projectiles;

    Vector2 projectileTargetPosition;

    public int attackRate;
    int makeAttack;

    // Use this for initialization
    void Start()
    {
        mScript = GameObject.Find("gameManager").GetComponent<gameManagerScript>();

        //add to a list of enemies
        mScript.enemyList.Add(this.gameObject);

        targetPosition = Vector2.zero;
        distanceFromCores = new Vector2(0, 1000);
        position = transform.position;
        isWalking = false;
        direction = -1;
        calculateTargetCore();
        hasMovedVertical = false;
        hasMovedHorizontal = false;
        health = 5;

        attackRate = 120;
    }

    void calculateTargetCore()
    {
        foreach (var t in mScript.GetComponent<gameManagerScript>().cores)
        {
            if ((transform.position - t.transform.position).magnitude < distanceFromCores.magnitude)
            {
                
                distanceFromCores = transform.position - t.transform.position;
                targetPosition = t.transform.position;
                projectileTargetPosition = t.transform.position;
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
        transform.position = position;

        if(isWalking == false)
        {
            MakeAttack();
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
                position.y += .01f;
            if (direction == 1)
                position.x -= .01f;
            if (direction == 2)
                position.y -= .01f;
            if (direction == 3)
                position.x += .01f;

            if (Mathf.Abs((targetPosition.y - position.y)) <= .01f|| Mathf.Abs((targetPosition.y - position.y)) <= -.01f)
            {
                position.y = position.y * 10;
                position.y = Mathf.Round(position.y);
                position.y = position.y / 10;
                isWalking = false;
                hasMovedVertical = true;
            }
            if (Mathf.Abs((targetPosition.x - position.x)) <= .01f || Mathf.Abs((targetPosition.x - position.x)) <= -.01f)
            {
                position.x = position.x * 10;
                position.x = Mathf.Round(position.x);
                position.x = position.x / 10;
                isWalking = false;
                hasMovedHorizontal = true;
            }
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
        //destroy enemy
        Destroy(gameObject);
        //call manager to reduce enemy count for wave
        //call manager to add money to player's bank etc
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
