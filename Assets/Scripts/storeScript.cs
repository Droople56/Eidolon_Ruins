using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storeScript : MonoBehaviour
{
    gameManagerScript manager;
    List<towerScript> towers;

    float towerAttackRate;
    Text tRateText;

    float projectileSize;

    int pSpeedCost = 100;
    Text pSpeedText;

    int pDamageCost = 100;
    Text pDamageText;

    float towerCost;

    //variables for instantiating new towers
    [HideInInspector] public float currentTowerSpeed = 0.02f;
    [HideInInspector] public int currentTowerDamage = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        manager = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
        towers = manager.towerList; //may need to add into update

        //tRateText = GameObject.Find("tRateText").GetComponent<Text>();
        pSpeedText = GameObject.Find("pSpeedText").GetComponent<Text>();
        Debug.LogWarning(pSpeedText.GetComponent<Text>().text);

        pDamageText = GameObject.Find("pDamageText").GetComponent<Text>();
    }

    //additional optional upgrade
    public void UpgradeProjectileSize()
    {
        
    }

    //Projectile Speed
    public void UpgradeProjectileSpeed()
    {
        if(manager.score >= pSpeedCost)
        {
            currentTowerSpeed += 0.01f;

            //upgrade towers
            foreach (towerScript tower in towers)
            {
                tower.speed = currentTowerSpeed;
            }

            //subtract cost of upgrade
            manager.score -= pSpeedCost;

            //increment cost
            pSpeedCost += 100;
        }
    }

    //Projectile Damage
    public void UpgradeProjectileDamage()
    {
        if (manager.score >= pDamageCost)
        {
            currentTowerDamage += 1;

            //upgrade towers
            foreach (towerScript tower in towers)
            {
                tower.damage = currentTowerDamage;
            }

            //subtract cost of upgrade
            manager.score -= pDamageCost;

            //increment cost
            pDamageCost += 100;
        }
    }

    public void BuyTower()
    {
        //same as above but instantiate towers
    }

    private void Update()
    {
        //update text
        pSpeedText.text = "Projectile Speed\n" + pSpeedCost;
        pDamageText.text = "Projectile Damage\n" + pDamageCost;
    }
}
