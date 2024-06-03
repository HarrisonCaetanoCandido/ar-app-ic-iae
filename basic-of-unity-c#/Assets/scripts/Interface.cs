using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
}

// Usamos interfaces para abstrair a implementação de métodos comuns a diferentes classes, para que elas tenham
// características parecidas, mas comportamentos distintos
// Também diminuímos o acoplamento, aumentamos a capacidade de manutenção,
// reutilização de código (ao iterar sobre diferentes objetos que são instanciados através da interface)
public interface Interactions
{
    public void Interact();
}

public class Door:MonoBehaviour, Interactions
{
    public void Interact()
    {
        print("The door has been opened");
    }
}
