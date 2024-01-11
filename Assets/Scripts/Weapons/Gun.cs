using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Turret
{

    [SerializeField] protected TrailRenderer tracerEffect;
    protected Ray ray;

    override public  void Shoot()
    {
        muzzleFlash.transform.parent = firePoint;
        muzzleFlash.transform.localEulerAngles = Vector3.zero;
        muzzleFlash.transform.localPosition = Vector3.zero;
        var tracer = Instantiate(tracerEffect, firePoint.position, Quaternion.identity);
        tracer.AddPosition(muzzleFlash.transform.position);
        muzzleFlash.Emit(1);
        ray.origin = firePoint.position;
        ray.direction = firePoint.position - target.position;
        hitEffect.transform.position = aimPoint.position;//target.position + new Vector3(0, (target.GetComponent<Collider>().bounds.size.y / 2), 0);
        hitEffect.transform.forward = transform.position - target.position;
        tracer.transform.position = hitEffect.transform.position;
        hitEffect.Emit(5);

        firePointIndex++;
        if (firePointIndex >= firePoints.Count)
        {
            firePointIndex = 0;
        }
        firePoint = firePoints[firePointIndex];

        target.GetComponent<EnemyHealthManager>().Damage(damage);
    }
}
