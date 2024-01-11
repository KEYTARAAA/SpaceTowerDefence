using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] float lerpSpeed;

    float health=75, maxHealth=100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setUp(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void changeHealth(float health)
    {
        this.health = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed * Time.deltaTime);

    }
}
