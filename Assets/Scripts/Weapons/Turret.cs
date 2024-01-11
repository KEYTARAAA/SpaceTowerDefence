using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static statics;

public class Turret : Display
{
    [SerializeField] protected int price;
    [SerializeField] protected float range = 50f, rotationSpeed = 10f, fireRate = 1f, damage = 10f;
    [SerializeField] protected WEAPONSETTINGS setting = WEAPONSETTINGS.CLOSESTTOEND;
    [SerializeField] protected Transform partToRotate, rangeObject;
    [SerializeField] protected List<Transform> firePoints;
    [SerializeField] protected ParticleSystem muzzleFlash, hitEffect;
    [SerializeField] protected string tag = "Enemy";
    protected Transform firePoint, target, aimPoint;
    protected int firePointIndex = 0;
    protected float fireCountdown = 0f;

    // Start is called before the first frame update
    protected void Start()
    {
        setContent2(damage.ToString());
        setContent3(fireRate.ToString());
        setContent4(price.ToString());
        InvokeRepeating("updateTarget", 0f, 0.5f);
        firePoint = firePoints[0];
        GetComponent<Display>().setContent1(name.Substring(0, name.Length-7));
    }

    // Update is called once per frame
    protected void Update()
    {

        Debug.DrawRay(partToRotate.position, partToRotate.forward * 100, Color.cyan);
        Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.magenta);
        if (GetComponent<MoveObject>().getPlaced()) {
            if (target == null)
            {
                return;
            }

            Rotate();


            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

    }

    void updateTarget()
    {
        switch (setting)
        {
            case (WEAPONSETTINGS.CLOSESTTOEND):
                updateTargetClosestToEnd();
                break;
                
            case (WEAPONSETTINGS.CLOSESTTOWEAPON):
                updateTargetClosestToWeapon();
                break;
                
            case (WEAPONSETTINGS.MOSTHEALTH):
                updateTargetMostHealth();
                break;

        }
    }

    void updateTargetClosestToEnd()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        float shortestDistance = Mathf.Infinity;
        GameObject closest = null;
        NavMeshAgent agent;
        float distance;

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.TryGetComponent<NavMeshAgent>(out agent)) 
            {
                enemy.transform.parent.TryGetComponent<NavMeshAgent>(out agent);
            }
            distance = RemainingDistance(agent.path.corners);
            
            if (distance < shortestDistance && Vector3.Distance(enemy.transform.position, transform.position) <= range)
            {
                shortestDistance = distance;
                closest = enemy;
            }
        }

        if (closest != null)
        {
            target = closest.transform;
            aimPoint = closest.transform.Find("AimPoint");
        }
        else
        {
            target = null;
        }
    }
    void updateTargetClosestToWeapon()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        float shortestDistance = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < shortestDistance )
            {
                shortestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                closest = enemy;
            }
        }

        if (closest != null && shortestDistance <= range)
        {
            target = closest.transform;
            aimPoint = closest.transform.Find("AimPoint");
        }
        else
        {
            target = null;
        }
    }
    void updateTargetMostHealth()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        float mostHealth = Mathf.Infinity;
        GameObject lowest = null;
        HealthManager healthManager;

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.TryGetComponent<HealthManager>(out healthManager))
            {
                enemy.transform.parent.TryGetComponent<HealthManager>(out healthManager);
            }

            if (healthManager.getHealth() < mostHealth && Vector3.Distance(enemy.transform.position, transform.position) <= range)
            {
                mostHealth = healthManager.getHealth();
                lowest = enemy;
            }
        }

        if (lowest != null)
        {
            target = lowest.transform;
            aimPoint = lowest.transform.Find("AimPoint");
        }
        else
        {
            target = null;
        }
    }

    public float RemainingDistance(Vector3[] points)
    {
        if (points.Length < 2) return 0;
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);
        return distance;
    }

    protected void Rotate()
    {
        Vector3 dir = aimPoint.position - partToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rot = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rot.x, rot.y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    virtual public void Shoot()
    {
    }

    public float getRange()
    {
        return range;
    }

    public int getPrice()
    {
        return price;
    }
    public void setPrice(int p)
    {
        price = p;
    }
    public virtual void placed()
    {

    }
}
