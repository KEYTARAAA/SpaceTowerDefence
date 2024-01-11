using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Display
{
    [SerializeField] float health, speed, damage;
    [SerializeField] int worth;
    LevelManager levelManager;

    private void Start()
    {
        GetComponentInChildren<DamageManager>().setDamage(damage);
        GetComponentInChildren<HealthManager>().setHealth(health);
        GetComponentInChildren<HealthManager>().setMaxHealth(health);
        GetComponentInChildren<EnemyHealthManager>().setWorth(worth);
        GetComponentInChildren<NavMeshAgent>().speed = speed;
    }

    public void setDamage(float f)
    {
        damage = f;
        GetComponentInChildren<DamageManager>().setDamage(damage);
    }

    public float getDamage()
    {
        return damage;
    }
    public void setHealth(float f)
    {
        health = f;
        GetComponentInChildren<HealthManager>().setHealth(health);
        GetComponentInChildren<HealthManager>().setMaxHealth(health);
    }

    public float getHealth()
    {
        return health;
    }
    public void setWorth(int i)
    {
        worth = i;
        GetComponentInChildren<EnemyHealthManager>().setWorth(worth);
    }

    public int getWorth()
    {
        return worth;
    }

    public void setLevelManager(LevelManager l)
    {
        levelManager = l;
        levelManager.addEnemy(this);
    }

    private void OnDestroy()
    {
        levelManager.removeEnemy(this);
    }

}
