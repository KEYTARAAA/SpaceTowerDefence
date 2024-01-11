using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    int worth;
    override public void Die()
    {
        if (transform.parent == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }

        PlayerMoneyManger.increaseMoney(worth);
    }

    public int getWorth()
    {
        return worth;
    }
    public void setWorth(int w)
    {
        worth = w;
    }
}
