using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractClass : MonoBehaviour
{
    public void Start()
    {
        Zombie v = new Zombie();
        v.Attack();
    }
}

// Classes abstratas não são instanciadas como objeto, mas servem como molde de classes filhas
public abstract class Villains
{
    private int force;
    private int speed;

    public Villains()
    {
        force = 100;
        speed = 50;
    }

    public abstract void Attack();
}

public class Zombie: Villains
{
    public override void Attack()
    {
        Debug.Log("Attacking");
    }
}