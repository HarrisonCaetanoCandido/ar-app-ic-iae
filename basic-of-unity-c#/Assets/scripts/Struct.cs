using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Struct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Something something;

        //something.dataOne = 1;
        //something.dataTwo = 2;

        something = new Something(1, 2);

        something.dataOne = 3;
        something.dataTwo = 4;
        something.OneMethod();
    }
}

// Quando queremos armazenar poucas informações em um objeto, nós podemos otimizar o uso de memória ao
// utilizar uma Struct, que contém menos propriedades do que uma classe
public struct Something
{
    public int dataOne;
    public int dataTwo;

    // Podemos criar um construtor numa struct para iniciar variáveis
    public Something(int dataOne, int dataTwo)
    {
        this.dataOne = dataOne;
        this.dataTwo = dataTwo;
    }

    public void OneMethod()
    {
        Debug.Log("Implementation");
    }
}