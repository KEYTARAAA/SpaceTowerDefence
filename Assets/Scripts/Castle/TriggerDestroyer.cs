using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroyer : MonoBehaviour
{
    [SerializeField] string layer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer))
        {
            DamageManager damageManager;
            if (other.TryGetComponent<DamageManager>(out damageManager))
            {
                GetComponentInParent<HealthManager>().Damage(damageManager.getDamage());
            }

            if (other.transform.parent == null)
            {
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.transform.parent.gameObject);
            }
            
        }
    }
}
