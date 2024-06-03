using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyObject;
    public float enemySpeed;

    // Start is called before the first frame update
    private void Main()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public void followPlayer()
    {
        Main();
        enemyObject.transform.position = Vector3.MoveTowards(enemyObject.transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }
}
