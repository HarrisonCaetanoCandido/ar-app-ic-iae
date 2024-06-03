using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Calculator c = gameObject.GetComponent<Calculator>();
        print(c.Sum(1,2));
        print(c.Sum(5.5f, 7.22f));
        print(c.Sum(5.5f, 7.22f, 9.95f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Calculator
{
    public int Sum(int a, int b)
    {
        return a + b;
    }

    public float Sum(float a, float b)
    {
        return a + b;
    }

    public float Sum(float a, float b, float c)
    {
        return a + b + c;
    }
}