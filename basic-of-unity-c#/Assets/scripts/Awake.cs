using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityAwake : MonoBehaviour
{
    // Podemos despertar um gameObject com este método, e ele é executado antes do void Start, sendo também executado apenas uma única vez
    private void Awake()
    {
        print("Awake");
    }

    void Start()
    {
        print("Start");
    }
}
