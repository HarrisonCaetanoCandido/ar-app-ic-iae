using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sealed_class : MonoBehaviour
{
    private A a = new A();
    void Start()
    {
        a.ShowSomething();
    }
}

// Uma classe selada não pode ser herdada por outra classe
// Motivo de uso: Ela não contém todas as características que uma classe normal possui,
// justamente pq pelo fato de não permitir a herança ela carrega menos propriedades na memória
// ou seja, temos uma otimizaçã
// o no nosso sistema
public sealed class A
{
    public void ShowSomething()
    {
        Debug.Log("Showing something...");
    }
}