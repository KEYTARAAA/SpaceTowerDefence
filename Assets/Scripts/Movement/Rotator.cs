using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 rotator;
    [SerializeField] bool stopped = true;
    Vector3 original;

    private void Start()
    {
        original = transform.localEulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        if (!stopped || !(((int)transform.localEulerAngles.x == (int) original.x) && ((int)transform.localEulerAngles.y == (int)original.y) && ((int)transform.localEulerAngles.z == (int)original.z) ) ) {
            Vector3 temp = transform.localEulerAngles;
            temp.x += rotator.x;
            temp.y += rotator.y;
            temp.z += rotator.z;
            transform.localEulerAngles = temp;
        }
    }

    public void reverse()
    {
        rotator *= -1;
    }

    public void stop()
    {
        stopped = true;
    }

    public void start()
    {
        stopped = false;
    }

}
