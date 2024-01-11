using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickManager : MonoBehaviour
{
    [SerializeField] Displayer displayer;
    GameObject obj;
    Outline outline;
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 1f;
    float timeDiff = 0;

        void Update()
        {
            // Check for mouse input
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow, 5f);
                // Casts the ray and get the first game object hit
                if(Physics.Raycast(ray, out hit))
                {
                    //Debug.Log(hit.transform.name);
                    clicked++;

                    if (clicked == 1)
                    {
                        clicktime = 1f / clickdelay;
                        timeDiff = Time.deltaTime - clicktime;
                        obj = hit.transform.gameObject;
                        if (obj.layer == LayerMask.NameToLayer("Display"))
                        {
                            if (outline != null)
                            {
                                outline.enabled = false;
                            }
                        outline = getDisplay(obj).transform.GetComponent<Outline>();
                        outline.enabled = true;
                        }
                    }


                    if (clicked > 1 && clicktime > 0 && getSameObj(obj,hit.transform.gameObject))
                    {
                        clicked = 0;
                        clicktime = -1;
                        if (obj.layer == LayerMask.NameToLayer("Display"))
                        {
                            displayer.Display(getDisplay(obj));
                        }

                    }
                    else if (!getSameObj(obj, hit.transform.gameObject))
                    {
                        obj = null;
                        clicked = 1;
                    }

                }
        }
        if (clicked > 2 || clicktime <= 0)
        {
            obj = null;
            clicked = 0;
        }
        clicktime -= Time.deltaTime;
        }

    Display getDisplay(GameObject g)
    {
        Display display;
        if (g.TryGetComponent<Display>(out display))
        {
            return display;
        }
        else
        {
            return getDisplay(g.transform.parent.gameObject);
        }
    }
    bool getSameObj(GameObject original, GameObject compare)
    {
        if (original == null)
        {
            return false;
        }
        if (original.name == compare.name)
        {
            return true;
        }
        else if (compare.transform.parent == null)
        {
            return false;
        }
        else
        {
            return getSameObj(original, compare.transform.parent.gameObject);
        }
    }



}
