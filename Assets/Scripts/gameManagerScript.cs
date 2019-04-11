using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
    public List<GameObject> cores;
    public List<GameObject> doorways;
    public float test;

    public List<GameObject> enemyList;

    public int waveNumber;
    public int enemiesRemaining;
    public int numEnemiesToSpawn;

    public List<towerScript> towerList;

    int spawnDirection;
    float spawnDirection2;
    public GameObject enemyReference;
    Vector2 spawnLocation;
    public int spawnTimer;

    GameObject nextWaveButton;

    [HideInInspector] public int score;
    [HideInInspector] public Text scoreText;
    [HideInInspector] public Text waveText;
    storeScript strScrpt;
    GameObject gameOverText;
    playerScript playerScript;
    GameObject restartButton;
    GameObject fireRateButton;
    GameObject damageUpgradeButton;
    // Use this for initialization
    void Awake()
    {
        cores = new List<GameObject>();

        playerScript = GameObject.Find("Player").GetComponent<playerScript>();
        strScrpt = GameObject.Find("gameManager").GetComponent<storeScript>();
        score = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);


        restartButton = GameObject.Find("Restart Button");
        fireRateButton = GameObject.Find("Projectile Speed Button");
        damageUpgradeButton = GameObject.Find("Projectile Damage Button");

        restartButton.SetActive(false);
        fireRateButton.SetActive(false);
        damageUpgradeButton.SetActive(false);

        waveNumber = 0;
        waveText.text = "Wave " + waveNumber;

        spawnLocation = Vector2.zero;
        spawnTimer = 0;

        nextWaveButton = GameObject.Find("NextWaveButton");

        //this method call will be moved later once we have a game loop that continually generates waves once the previous is beaten
        setupWave();
        
        enemyList = new List<GameObject>();
        //cores = new List<GameObject>();
        towerList = new List<towerScript>();
        
    }


    // Update is called once per frame
    void Update()
    {
        spawnEnemies();
    }

    public void setupWave()
    {
        //increases wave number, sets number of enemies to be spawned and syncs enemies remaining with that number, resets spawn timer to zero
        waveNumber++;
        waveText.text = "Wave " + waveNumber;

        numEnemiesToSpawn = waveNumber + 2;
        enemiesRemaining = numEnemiesToSpawn;
        spawnTimer = 0;

        //disable button
        nextWaveButton.SetActive(false);
        fireRateButton.SetActive(false);
        damageUpgradeButton.SetActive(false);
        //enable towers
        foreach (towerScript tower in towerList)
        {
            tower.enabled = true;
        }
    }

    void ShopScreen()
    {
        //disable our towers
        foreach(towerScript tower in towerList)
        {
            tower.enabled = false;
        }

        //show the next wave button
        nextWaveButton.SetActive(true);
        fireRateButton.SetActive(true);
        damageUpgradeButton.SetActive(true);
        
    }

    //method to spawn enemies on outer ring on random tiles (outer ring is ((-3,-2) (-3,2) (3,-2) (3,2)) only spawn on tenths aka (-3,.6) or (.3,2)
    void spawnEnemies()
    {
        spawnTimer++;
        //all of this only happens if the timer reaches a certain point AND there are enemies left to spawn in the wave
        if (numEnemiesToSpawn > 0&&spawnTimer==120)
        {
            //select random number to see if spawn on top, left, bottom, or right of screen
            spawnDirection = Random.Range(0, 5);

            //depending on selection, enemy is placed on one of the sides of the outer spawning rectangle
            if (spawnDirection == 0)
            {
                spawnLocation.y = 2;
                spawnLocation.x = -1;
            }
            else if (spawnDirection == 1)
            {
                spawnLocation.x = -3;
                spawnLocation.y = -.2f;
            }
            else if (spawnDirection == 2)
            {
                spawnLocation.y = -2;
                spawnLocation.x = -0.6f;
            }
            else if (spawnDirection == 3)
            {
                spawnLocation.x = 3;
                spawnLocation.y = 0.4f;
            }
            else if (spawnDirection == 4)
            {
                spawnLocation.y = -2;
                spawnLocation.x = 1.2f;
            }

            //spawn enemy using this location
            enemyList.Add(Instantiate(enemyReference, spawnLocation, Quaternion.identity));

            //reduce enemies left to spawn by one
            numEnemiesToSpawn--;

            //reset spawnTimer
            spawnTimer = 0;
        }

        if (enemiesRemaining == 0)
        {
            //sets up next wave's numbers
            ShopScreen();
            //call method that is while loop containing the shop
        }

        if(cores.Count == 0)
        {
            GameOver();
        }
            

    }

    private void GameOver()
    {
        //disable player script
        playerScript.enabled = false;

        //display gameOver text & restart button
        gameOverText.GetComponent<Text>().text = "Game Over\nYou reached Wave " + waveNumber;
        gameOverText.SetActive(true);
        restartButton.SetActive(true);

        //disable other text items
        scoreText.gameObject.SetActive(false);
        waveText.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        //load the menu screen
        SceneManager.LoadScene(0);
    }
}
