using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : Turret
{
    [SerializeField] protected Dictionary<Transform, Transform> projectiles;
    [SerializeField] Transform projectile;
    [SerializeField] Vector3 projectileRotation;
    [SerializeField] Vector3 scaler;
    [SerializeField] protected float projectileSpeed = 7f, damageRadius = 7f;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        projectiles = new Dictionary<Transform, Transform>();
    }

    void SpawnProjectile(Transform t)
    {
        Transform m = Instantiate(projectile, t.position, t.rotation, t);
        m.GetComponent<Projectile>().setBaseLauncher(transform);
        m.localEulerAngles = projectileRotation;
        m.localScale = scaler;
        m.GetComponent<Projectile>().setDamage(damage);
        m.GetComponent<Projectile>().setSpeed(projectileSpeed);
        m.GetComponent<Projectile>().setDamageRadius(damageRadius);
        m.GetComponent<Projectile>().setHitEffect(hitEffect.gameObject);
        m.GetComponent<MaterialManager>().addMaterial(PlayerMaterialManager.getHDRMaterial(), false, false);
        projectiles[t] = m;
    }


    override public void Shoot()
    {
        projectiles[firePoint].GetComponent<Projectile>().Launch();
        ParticleSystem mf = Instantiate(muzzleFlash, projectiles[firePoint].position, Quaternion.Euler(0, 0, 0));
        var main = mf.main;
        main.stopAction = ParticleSystemStopAction.Destroy;
        //muzzleFlash.transform.position = missiles[firePoint].position;//new Vector3(firePoint.position.x, firePoint.position.y, muzzleFlash.transform.position.z);
        mf.Play();
        SpawnProjectile(firePoint);

        firePointIndex++;
        if (firePointIndex >= firePoints.Count)
        {
            firePointIndex = 0;
        }
        firePoint = firePoints[firePointIndex];
    }

    public override void placed()
    {
        foreach (Transform t in firePoints)
        {
            SpawnProjectile(t);
        }
    }
}
