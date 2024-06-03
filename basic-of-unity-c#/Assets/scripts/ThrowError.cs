using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ThrowError : MonoBehaviour
{
    private int n1 = 0;

    private void Start()
    {
        if (n1 < 0)
            throw new System.Exception("The number provided is negative");
    }
}
