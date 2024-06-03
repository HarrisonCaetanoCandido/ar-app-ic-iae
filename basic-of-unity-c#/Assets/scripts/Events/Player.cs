using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        // Lembrando, um evento não pode ser acessado por outra classe
        Villain.OnEnemyDeath += this.Cheer;
    }

    private void Cheer()
    {
        print("Cheering");
    }
}
