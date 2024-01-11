using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 1f, damage, damageRadius;
    [SerializeField] Transform baseLauncher;
    GameObject hitEffect;

    public abstract void Launch();
    public void setDamageRadius(float d)
    {
        damageRadius = d;
    }
    public void setDamage(float d)
    {
        damage = d;
    }
    public void setSpeed(float s)
    {
        speed = s;
    }

    public void setHitEffect(GameObject g)
    {
        hitEffect = g;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!isInParent(collision.transform, baseLauncher))
        {


            Collider[] inRadius = Physics.OverlapSphere(transform.position, damageRadius);
            foreach (Collider c in inRadius)
            {
                if (c.transform.TryGetComponent<EnemyHealthManager>(out EnemyHealthManager enemyHealthManager))
                {
                    enemyHealthManager.Damage(damage);
                }
            }

            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {

        GameObject g = Instantiate(hitEffect, transform.position, transform.rotation);
        g.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x, transform.localScale.x);
    }

    public void DeactivateAnimator()
    {
        transform.GetComponent<Animator>().enabled = false;
    }

    bool isInParent(Transform compare, Transform parent)
    {
        if (compare == parent)
        {
            return true;
        }

        bool inKids = false;

        foreach(Transform child in parent)
        {
            if (isInParent(compare, child))
            {
                inKids = true;
            }

        }

        return inKids;
    }

    public void setBaseLauncher(Transform t)
    {
        baseLauncher = t;
    }
}
