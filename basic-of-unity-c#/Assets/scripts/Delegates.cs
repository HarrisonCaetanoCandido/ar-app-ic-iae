using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Com delegates podemos referenciar um método por uma variável de mesma assinatura (tipo e parâmetros)
public class Delegates : MonoBehaviour
{
    // Defino um tipo (assinatura)
    private delegate void Example();
    // para instanciar um objeto com a mesma assinatura
    private Example variableDelegate;
    
    void Start()
    {
        variableDelegate = this.Attack;
        variableDelegate();

        // A grande utilidade do delegate é poder fazer a chamada de mais de um método de uma vez
        variableDelegate += this.Jump;
        variableDelegate += this.Defend;
        variableDelegate();
    }

    private void Attack()
    {
        print("Atacking");
    }

    private void Jump()
    {
        print("Im Jumping");
    }

    // Podemos ainda passar uma variável delegate como parâmetro de métodos
    private void Defend(Example deleg)
    {
        print("Im defending myself");
    }
}