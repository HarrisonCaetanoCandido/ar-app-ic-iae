using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSet : MonoBehaviour
{
    Properties p = new Properties();
    private int x;

    void Start()
    {
        // Dou um get
        x = p.Number;

        // Dou um set
        p.Number = 5;

        // Dou um set novamente
        p.Number = 15;
    }
}

public class Properties
{
    private int number = 10;

    public int Number
    {
        get { return number; }
        set { if(value > number) number = value; }
    }
}