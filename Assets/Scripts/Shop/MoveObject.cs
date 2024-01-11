using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static statics;

public class MoveObject : MonoBehaviour
{
    [SerializeField] GameObject rangeObject;
    [SerializeField] float distance = 1000f, floorDistance = 3;
    [SerializeField] Vector3 placement = new Vector3(-2.5f, 0,  2.5f);
    [SerializeField] string layerName= "Weapon Port";
    GameObject explosion;

    Material red, green;
    [SerializeField] bool placed = false, okPlacement = false;
    float rayY, rayD = 100, range;
    TurretPortManager turretPortManager;

    private void Start()
    {
        rayY = GetComponent<Collider>().bounds.size.y;
        GetComponent<Collider>().enabled = false;
        red = Resources.Load("Materials/Hollow/PulseRed") as Material;
        green = Resources.Load("Materials/Hollow/PulseGreen") as Material;
        gameObject.GetComponent<MaterialManager>().addMaterial(red, false, true);
        explosion = Resources.Load("Models/FX/FX_Explosion") as GameObject;
    }
    public void Update()
    {
        if (!placed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                transform.position = hit.point;
            }
            else
            {
                transform.position = ray.origin + (ray.direction * distance);
            }

            ray = new Ray (transform.position + new Vector3(0, rayY,0), Vector3.down);
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction * rayD), Color.yellow);
            if (Physics.Raycast(ray, out hit,rayD))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer(layerName) && hit.transform.TryGetComponent<TurretPortManager>(out turretPortManager) && GetComponent<Turret>().getPrice() <= PlayerMoneyManger.getMoney())
                {
                    if (!turretPortManager.getOccupied()) {
                        gameObject.GetComponent<MaterialManager>().addMaterial(green, false, true);
                        transform.parent = hit.transform;
                        transform.localPosition = placement;
                        okPlacement = true;
                    }
                    else
                    {
                        gameObject.GetComponent<MaterialManager>().addMaterial(red, false, true);
                        okPlacement = false;
                    }
                }
                else
                {
                    gameObject.GetComponent<MaterialManager>().addMaterial(red, false,true);
                    okPlacement = false;
                }
            }
            else
            {
                gameObject.GetComponent<MaterialManager>().addMaterial(red, false,true);
                okPlacement = false;
            }


            if (Input.GetButtonUp("Fire1"))
            {
                release();
            }


        }
        else
        {
            return;
        }
    }

    

    public void release()
    {
        if (okPlacement )
        {
            if (!placed) {
                placed = true;
                GetComponent<MaterialManager>().addMaterial(PlayerMaterialManager.getHDRMaterial(), false, false);
                turretPortManager.setOccupied(true);
                rangeObject.SetActive(false);
                transform.parent.GetComponent<MaterialManager>().AddPart(GetComponent<Renderer>());
                PlayerMoneyManger.decreaseMoney(GetComponent<Turret>().getPrice());
                GetComponent<Collider>().enabled = true;
                LevelManager.turrets.Add(this);
                SetLayerRecursively(gameObject, "Display");
                GetComponent<Turret>().placed();
                //enabled = false;
            }
            else
            {

            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void displayRange()
    {
        rangeObject.SetActive(true);
        range = GetComponent<Turret>().getRange() * 2;
        rangeObject.transform.localScale = new Vector3(range, rangeObject.transform.localScale.y, range);
    }

    public void setRangeObject(GameObject r)
    {
        rangeObject = r;
    }

    private void OnMouseUp()
    {
        release();
    }

    private void OnDestroy()
    {
        if (turretPortManager != null) 
        {
            turretPortManager.setOccupied(false);
        }
        Instantiate(explosion, transform.position, transform.rotation);
    }

    public bool getPlaced()
    {
        return placed;
    }
}
