using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : Projectile
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void Launch()
    {
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = speed * transform.parent.forward;
    }
}
