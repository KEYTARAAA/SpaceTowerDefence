using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    float damage = 5;

    public float getDamage()
    {
        return damage;
    }

    public void setDamage(float d)
    {
        damage = d;
    }
}
