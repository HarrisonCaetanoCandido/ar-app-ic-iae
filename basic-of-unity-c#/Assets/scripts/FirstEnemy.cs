using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstEnemy : MonoBehaviour
{
    Enemies En = new Enemies();

    // Start is called before the first frame update
    void Start()
    {
        En.enemyObject = this.gameObject;
        En.enemySpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        En.followPlayer();
    }
}
