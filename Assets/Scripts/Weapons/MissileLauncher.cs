using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : ProjectileLauncher
{
    override public void Shoot()
    {
        projectiles[firePoint].GetComponent<Missile>().setTarget(target);
        base.Shoot();
    }
}
