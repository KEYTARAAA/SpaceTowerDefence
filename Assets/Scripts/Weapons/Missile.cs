using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    [SerializeField] Transform target;
    [SerializeField] bool launched = false;
    [SerializeField] Vector3 destination;
    // Start is called before the first frame update
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            destination = target.Find("AimPoint").position;
            Debug.DrawLine(transform.position, destination, Color.yellow);
        }

        if (launched)
        {
            transform.position += (destination - transform.position).normalized * speed * Time.deltaTime;
            transform.up = -(transform.position - destination);

            if (target == null)
            {
                if (Physics.Raycast(destination, -Vector3.up, out RaycastHit hit))
                {
                    destination = hit.point;
                    Debug.DrawLine(transform.position, hit.point, Color.cyan);
                }
            }

        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

    public override void Launch()
    {
        launched = true;
    }

    public void setTarget(Transform t)
    {
        target = t;
    }

    public void setDestination( Vector3 d)
    {
        destination = d;
    }
}
