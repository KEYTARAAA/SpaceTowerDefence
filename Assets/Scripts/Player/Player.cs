using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health = 100;


    public float getHealth()
    {
        return health;
    }
}
