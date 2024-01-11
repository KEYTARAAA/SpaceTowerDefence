using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class ImageToObject : MonoBehaviour
{

    public static float distance = 1000f;
    bool click = false;
    public void Drag()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            GameObject g;
                if (Physics.Raycast(ray, out hit, distance))
                {
                    g = Instantiate(Resources.Load("Models/Guns/" + name) as GameObject, hit.point, Quaternion.identity);

                }
                else
                {

                    g = Instantiate(Resources.Load("Models/Guns/" + name) as GameObject, ray.origin + (ray.direction * 20), Quaternion.identity);
                }
                GameObject r = Instantiate(Resources.Load("Models/Range") as GameObject, g.transform);
        //r.transform.localPosition = r.transform.position;
        g.GetComponent<MaterialManager>().addMaterial(Resources.Load("Materials/Hollow/PulseRed") as Material, false, true);
        g.GetComponent<MoveObject>().setRangeObject(r);
        g.GetComponent<MoveObject>().displayRange();
        SetLayerRecursively(g, "Ignore Raycast");
    }


    private void OnMouseDown()
    {
        Debug.Log("DOWN");
        click = true;
    }

    private void Update()
    {
        if (click &&  !PointerOverUICheck.IsPointerOverUIElement())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            GameObject g;
                if (Physics.Raycast(ray, out hit, distance))
                {
                    g = Instantiate(Resources.Load("Models/Guns/" + name) as GameObject, hit.point, Quaternion.identity);

                }
                else
                {

                    g = Instantiate(Resources.Load("Models/Guns/" + name) as GameObject, ray.origin + (ray.direction * 20), Quaternion.identity);
                }
                g.GetComponent<MaterialManager>().addMaterial(Resources.Load("Materials/Hollow/PulseRed") as Material, false, true);
            click = false;
        }
    }


}
