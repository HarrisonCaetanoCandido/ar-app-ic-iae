using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overwritten : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// A sobrescrita te permite complementar métodos de uma classe em outras
public class SomeMonster: MonoBehaviour
{
     // A palavra "virtual" permite a sobrescrita de metodos
    public virtual void Attack()
    {
        // Implementação base
        print("Hello, im some monster created from another monster");
    }
}

public class Frankenstein: SomeMonster
{
    public override void Attack()
    {
        base.Attack();
        // Aplico uma nova implementação a esse método
    }
}