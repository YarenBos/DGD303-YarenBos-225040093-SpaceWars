using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public static BossManager instance;
    public string bossName;

    public int currentHealth = 100;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.bossName.text = bossName;
        UIManager.Instance.bossHealthSlider.maxValue = currentHealth;
        UIManager.Instance.bossHealthSlider.value = currentHealth;
        UIManager.Instance.bossHealthSlider.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtBoss()
    {
        currentHealth--; 

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            UIManager.Instance.bossHealthSlider.gameObject.SetActive(false);
        }
    }
}
