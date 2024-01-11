using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Displayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TB1, TB2, TB3, TB4, Header5, Content5;
    [SerializeField] Transform camera;
    [SerializeField] Vector3 pos, rot;

    public void Display(Display d)
    {
        GetComponent<Animator>().SetBool("opening", true);
        TB1.text = d.getHeader1() + ": " + d.getContent1();
        TB2.text = d.getHeader2() + ": " + d.getContent2();
        TB3.text = d.getHeader3() + ": " + d.getContent3();
        TB4.text = d.getHeader4() + ": " + d.getContent4();
        Header5.text = d.getHeader5();
        Content5.text = d.getContent5();

        camera.position = d.transform.position;
        camera.position = camera.position + pos;
        camera.LookAt(d.transform);

        //camera.parent = transform;
    }

    public void Hide()
    {
        GetComponent<Animator>().SetBool("opening", false);
    }
}
