using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] HealthUI ui;
    float maxHealth;
    [SerializeField] float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (ui != null)
        {
            ui.setUp(maxHealth);
        }
    }

    public void Heal( float change )
    {
        health += change;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (ui != null)
        {
            ui.changeHealth(health);
        }
        
    }

    public void Damage( float change )
    {
        health -= change;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (ui != null)
        {
            ui.changeHealth(health);
        }
        
    }

    virtual public void Die()
    {
        
    }

    public void setUI(HealthUI ui)
    {
        this.ui = ui;
        this.ui.setUp(maxHealth);
    }

    public float getHealth()
    {
        return health;
    }
    public void setHealth(float h)
    {
        health = h;

        if (ui != null)
        {
            ui.changeHealth(health);
        }
    }
    public void setMaxHealth(float h)
    {
        maxHealth = h;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }
}
