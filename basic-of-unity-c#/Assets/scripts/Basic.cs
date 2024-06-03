using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    private int playerLife = 10;
    private int attackEnemy = 5;
    private float jumpForce = 5.5f;
    private bool playerDead = false;
    private string playerName = "Frederico";
    private int[] v = { 10, 20, 30 };
    private string[] s;
    private List<int> list = new List<int>();
    private Dictionary<string, string> villains = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        //print(playerLife);
        print(Sum(6, 5));
        print(Multiply(5, 5));
        playerLife = 40;
        print("Initially: " + playerLife);
        playerLife = Sub(playerLife, 10);
        print("Before suffer 10 of hit, the player life is " + playerLife);

        for (int i = 0; i < v.Length; i++)
            print("v: " + v[i]);

        // Fazemos alocação dinâmica de memória aqui
        s = new string[v.Length];
        s[0] = playerName;
        print(s[0]);

        foreach (string i in s)
            print(i);

        list.Add(3);
        list.Add(2);
        list.Add(1);
        list.Add(1);
        list.Sort();
        // Remove um elemento a partir do indice 0
        list.RemoveRange(0, 1);

        foreach (int l in list)
            print(l);

        print(list.LastIndexOf(1));
        print(list.Count);

        // Os métodos de dicionário são parecidos com os métodos de lista
        villains.Add("reaper", "reaper is the first phase villain " + "height: 180cm " + "force: 200 " + "speed: 20");
        villains.Add("terminator", "terminator is the second phase villain " + "height: 200cm " + "force: 500 " + "speed: 50");
        print(villains["reaper"]);
        print(villains["terminator"]);
        villains.Remove("terminator");
        print(villains.Count);
    }

    // Update is called once per frame
    void Update()
    {
        // Print só pode ser utilizado em classes que herdam da classe MonoBehaviour
        //print(playerLife);
    }

     int Sum(int x, int y)
    {
        int result;
        result = x + y;

        // O mesmo comportamento que print
        Debug.Log(result);
        return x + y;
    }

    int Multiply(int x, int y)
    {
        return x * y;
    }

    int Sub(int x, int y)
    {
        return x - y;
    }
}

// print não é definido aqui, pois é um método herdado de MonoBehaviour
public class Vampires: MonoBehaviour
{
    int force;
    int speed;

    void Attack()
    {
        print("");
    }
}

// Herda de Vampires e herda de MonoBehaviour
class Werewolf: Vampires
{
    // Crio um start local para esta classe
    private void Start()
    {
        print("");    
    }
}