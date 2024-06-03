using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{
    public Vamps dracula;
    public AnotherMonster monster;

    // Start is called before the first frame update
    void Start()
    {
        // Essa é a maneira adequada de instanciar novos objetos no projeto unity
        dracula = gameObject.AddComponent<Vamps>();
        dracula.Init(100, 50);
        monster = gameObject.AddComponent<AnotherMonster>();
        monster.Init(150, 50);
    }

    // Update is called once per frame
    void Update()
    {
        dracula.Attack();
    }
}

public class Vamps: MonoBehaviour
{
    public int force;
    public int speed;

    // Usamos init como nome do construtor, pois ele herda de MonoBehaviour, cujo construtor não pode ser substituido
    public void Init(int m, int n)
    {
        int s, r;
        force = m;
        speed = n;

        s = Statics.sum(5, 10);
        r = s > 10 ? 15 : s;

        print(r);
    }

    public void Attack()
    {
        print("Attack");
    }
}

public class AnotherMonster: Vamps
{
    // O construtor não pode ter o nome da classe, pois a classe pai herda de MonoBehaviour, cujo construtor não
    // pode ser substituido
    public void Init(int m, int n)
    {
        base.Init(m, n);
    }
}