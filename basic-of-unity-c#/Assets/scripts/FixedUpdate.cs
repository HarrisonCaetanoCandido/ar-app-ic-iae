using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityFixedUpdate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update: " + Time.deltaTime);
    }

    // FixedUpdate é chamado de maneira constante num intervalo de tempo
    // independente de quantos frames tiverem naquele segundo
    // É mais útil em jogos que trabalham com física, pois ele é mais preciso, o que é necessário para
    // os calculos
    private void FixedUpdate()
    {
        Debug.Log("Fixed Update: " + Time.deltaTime);
    }
}
