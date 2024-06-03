using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Busco um método com o mesmo nome da string passada como argumento após 2.5 segundos da execução
        // dessa instrução. 
        // Problema: Ele consome bastante memória e pode deixar o programa lento
        Invoke("Invoked", 2.5f);
    }

    private void Invoked()
    {
        print("Invoked");
    }
}
