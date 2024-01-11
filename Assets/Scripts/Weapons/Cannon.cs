using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : ProjectileLauncher
{
    [SerializeField] bool lowA = true;
    private void Update()
    {
        base.Update();
        if (target == null)
        {
            return;
        }
        RotateHead();
    }

    void RotateHead()
    {
        float? angle = CalculateAngle(lowA);
        if (angle!=null)
        {
            partToRotate.localEulerAngles = new Vector3(360f - (float)angle, partToRotate.localEulerAngles.y, partToRotate.localEulerAngles.z);
        }
    }

    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = target.position - partToRotate.position;
        float y = targetDir.y;
        targetDir.y = -.203f;
        float x = targetDir.magnitude - 1.134f;
        float gravity = 9.8f;
        float sSqr = projectileSpeed * projectileSpeed;
        float underSqrRoot = (sSqr * sSqr) - gravity * ((gravity * x * x) + (2 * y * sSqr));
        if (underSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;
            if (low)
            {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
        {
            return null;
        }

    }
}
