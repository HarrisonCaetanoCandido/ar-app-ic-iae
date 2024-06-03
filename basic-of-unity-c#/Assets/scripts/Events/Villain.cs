using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    public delegate void EnemyDeath();
    // Sempre colocamos On + estado do objeto
    public static event EnemyDeath OnEnemyDeath;
    public int enemyLife = 100;

    void Start()
    {
        if(enemyLife == 0)
            if(OnEnemyDeath!=null)
                // Vou notificar meu jogador que o vilão morreu
                OnEnemyDeath();
        
    }
}
