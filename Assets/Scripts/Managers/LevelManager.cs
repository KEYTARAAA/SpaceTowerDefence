using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int amountBoost = 5, statBoost = 10;
    [SerializeField] float wait = 5;
    public static HealthManager health;
    [SerializeField] EnemyPlacementManager enemyPlacementManager;
    [SerializeField] TextMeshProUGUI text;
    List<Enemy> enemies;
    public static List<MoveObject> turrets;
    int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
        turrets = new List<MoveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addEnemy(Enemy e)
    {
        enemies.Add(e);
    }
    public void removeEnemy(Enemy e)
    {
        enemies.Remove(e);

        if (enemies.Count == 0 && health.getHealth() > 0)
        {
            LevelUp();
        }
        else if (health.getHealth() <= 0)
        {
            Die();
        }
    }

    public void LevelUp()
    {
        level += 1;
        if (level % amountBoost == 0)
        {
            enemyPlacementManager.setAmount(enemyPlacementManager.getAmount() + 1);
        }
        if (level % statBoost == 0)
        {
            enemyPlacementManager.increaseMultiplier();
        }

        text.text = "Level " + level;
    }

    public void Die()
    {
        foreach (Enemy e in enemies)
        {
            Destroy(e.gameObject);
        }
        enemies.Clear();


        if (turrets.Count > 0)
        {
            Destroy(turrets[Random.Range(0, turrets.Count)].gameObject);
        }

        Invoke("ResetHealth", wait);
    }

    void ResetHealth()
    {
        health.setHealth(health.getMaxHealth());
    }
}
