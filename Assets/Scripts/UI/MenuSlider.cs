using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlider : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void toggleMenu()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.name != name)
            {
                child.GetComponent<Animator>().SetBool("hiding", !child.GetComponent<Animator>().GetBool("hiding"));
            }
        }
        animator.SetBool("opening", !animator.GetBool("opening"));
    }
}
