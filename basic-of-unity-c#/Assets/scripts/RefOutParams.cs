using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// MODIFICADORES DE PARÂMETRO: REF, OUT PARAMS
public class RefOutParams : MonoBehaviour
{
    private int n;
    private int sub;

    // Start is called before the first frame update
    void Start()
    {
        n = 10;
        Example(10);
        print(Example(10, 5, out sub));
        print(Example(10, 20, 30, 50, 40));
    }

    // Passagem de parametros normal (passagem de valor de uma variável para outra)
    private void Example(int n)
    {
        print(n);
    }

    // O problema com a solução anterior é que ela usa dois espaços de memória diferentes (duas variáveis) para
    // trabalhar com o mesmo valor, podemos usar o modificador de parâmetro Ref para fazer uma referência/ apontar
    // para a variável original e trabalhar apenas com seu valor no método, em vez de criar uma variável adicional que
    // aponte para tal. É quase um ponteiro em C, a diferença é que não criamos uma variável que aponta para outra, mas
    // sim passamos literalmente o endereço de memória onde aquela variável está na memória

    private void Example (ref int n)
    {
        print(n);
    }

    // Out aqui permite um retorno direto no endereço de memória da variável passada como parâmetro, pode permitir
    // mais de um retorno
    private int Example(int a, int b, out int sub)
    {
        print(a + b);
        sub = a - b;
        print(sub);
        return a + b;
    }

    private int Example(params int [] n)
    {
        int sum, i;

        sum = 0;
        for(i = 0; i < n.Length; i++)
            sum += n[i];

        return sum;
    }
}
