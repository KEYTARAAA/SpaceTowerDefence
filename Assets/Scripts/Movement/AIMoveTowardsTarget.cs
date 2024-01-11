using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveTowardsTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float y = 0;
    GameObject explosion;
    bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        explosion = Resources.Load("Models/FX/FX_Explosion") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            moving = !moving;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(gameObject);
        }

        if (moving)
        {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(target.position);
        }

        /*if (gameObject.GetComponent<NavMeshAgent>().remainingDistance < .1 && target != null)
        {
            Destroy(gameObject);
        }*/
    }

    public void setTarget(Transform t)
    {
        target = t;
        moving = true;
    }

    public float getY()
    {
        return y;
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
