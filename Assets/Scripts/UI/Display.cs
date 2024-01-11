using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField]string Header1, Header2, Header3, Header4, Header5;
    [SerializeField] [TextArea]string Content1, Content2, Content3, Content4, Content5;

    public string getHeader1()
    {
        return Header1;
    }
    public string getHeader2()
    {
        return Header2;
    }
    public string getHeader3()
    {
        return Header3;
    }
    public string getHeader4()
    {
        return Header4;
    }
    public string getHeader5()
    {
        return Header5;
    }
    public string getContent1()
    {
        return Content1;
    }
    public string getContent2()
    {
        return Content2;
    }
    public string getContent3()
    {
        return Content3;
    }
    public string getContent4()
    {
        return Content4;
    }
    public string getContent5()
    {
        return Content5;
    }
    public void setHeader1(string s)
    {
        Header1 = s;
    }
    public void setHeader2(string s)
    {
        Header2 = s;
    }
    public void setHeader3(string s)
    {
        Header3 = s;
    }
    public void setHeader4(string s)
    {
        Header4 = s;
    }
    public void setHeader5(string s)
    {
        Header5 = s;
    }
    public void setContent1(string s)
    {
        Content1 = s;
    }
    public void setContent2(string s)
    {
        Content2 = s;
    }
    public void setContent3(string s)
    {
        Content3 = s;
    }
    public void setContent4(string s)
    {
        Content4 = s;
    }
    public void setContent5(string s)
    {
        Content5 = s;
    }
}
