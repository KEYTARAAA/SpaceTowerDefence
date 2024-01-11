using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Turret
{
    [SerializeField] Transform[] laserRotators;
    [SerializeField] float tickTimer, laserRotatorSpeed = 5f;
    bool damageOn = false, shooting = false;
    [SerializeField] Rotator[] rotators;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        for (int i = 0; i < rotators.Length; i++)
        {
            if (i % 2 == 1 )
            {
                rotators[i].reverse();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MoveObject>().getPlaced())
        {
            if (target == null)
            {
                if (shooting == true)
                {
                    shooting = false;
                    foreach (Transform f in firePoints)
                    {
                        f.GetComponent<Hovl_Laser>().DisablePrepare();
                    }

                    foreach (Rotator r in rotators)
                    {
                        r.stop();
                    }
                }
                else if (laserRotators.Length > 0)
                {
                    if ((int)laserRotators[0].localEulerAngles.z != 0)
                    {
                        for (int i = 0; i < laserRotators.Length; i++)
                        {
                            Transform t = laserRotators[i];
                            float rot = 0;
                            if (i % 2 == 0)
                            {
                                rot = laserRotatorSpeed;
                            }
                            else
                            {
                                rot = -laserRotatorSpeed;
                            }
                            t.Rotate(new Vector3(0, 0, rot));
                        }
                    }
                }
                return;
            }

            Rotate();

            if (shooting == false)
            {
                Shoot();
            }

            if (shooting)
            {
                for(int i = 0; i < laserRotators.Length; i++)
                {
                    Transform t = laserRotators[i];
                    float rot = 0;
                    if (i % 2 == 0)
                    {
                        rot = laserRotatorSpeed;
                    }
                    else
                    {
                        rot = -laserRotatorSpeed;
                    }
                    t.Rotate(new Vector3(0,0,rot));
                }


                if (damageOn && firePoints[0].GetComponent<Hovl_Laser>().getTarget() != null)
                {
                    if (firePoints[0].GetComponent<Hovl_Laser>().getTarget().TryGetComponent<EnemyHealthManager>(out EnemyHealthManager e)) 
                    {
                        damageOn = false;
                        e.Damage(damage);
                        StartCoroutine(Tick());
                    }
                }


            }

            foreach (Transform f in firePoints)
            {
                f.LookAt(aimPoint);
            }

        }
    }

    override public void Shoot()
    {

        shooting = true;
        damageOn = true;
        foreach (Transform f in firePoints)
        {
            f.GetComponent<Hovl_Laser>().EnablePrepare();
            f.GetComponent<Hovl_Laser>().setMaxLength(range);
        }
        foreach (Rotator r in rotators)
        {
            r.start();
        }
    }

    IEnumerator Tick()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(fireRate);
        damageOn = true;
    }
}
