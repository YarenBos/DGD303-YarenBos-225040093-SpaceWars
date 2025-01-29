using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currentHealth;
    public int maxHealth;

    public GameObject deathEffect;

    public float invincibleLenght = 2f;
    private float invincCounter;
    public SpriteRenderer theSR;

    public int shieldPwr;
    public int shieldMaxPwr = 2;
    public GameObject theShield;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIManager.Instance.healthBar.maxValue = maxHealth;
        UIManager.Instance.healthBar.value = currentHealth;

        UIManager.Instance.shieldBar.maxValue = shieldMaxPwr;
        UIManager.Instance.shieldBar.value = shieldPwr;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCounter >= 0)
        {
            invincCounter -= Time.deltaTime;

            if(invincCounter <= 0 )
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    public void HurtPlayer()
    {
        if (invincCounter <= 0)
        {

            if (theShield.activeInHierarchy)
            {
                shieldPwr--;

                if( shieldPwr <= 0 )
                {
                    theShield.SetActive(false);
                }
                UIManager.Instance.shieldBar.value = shieldPwr;
            }
            else
            {

                currentHealth--;

                UIManager.Instance.healthBar.value = currentHealth;

                if (currentHealth <= 0)
                {
                    Instantiate(deathEffect, transform.position, transform.rotation);
                    gameObject.SetActive(false);

                    GameManager.Instance.KillPlayer();

                    WaveManager.instance.canSpawnWaves = false;
                }

                PlayerController.instance.doubleShotActive = false;
            }
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        UIManager.Instance.healthBar.value = currentHealth;

        invincCounter = invincibleLenght;
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
    }

    public void ActivateShield()
    {
        theShield.SetActive(true);
        shieldPwr = shieldMaxPwr;

        UIManager.Instance.shieldBar.value = shieldPwr;
    }
}
