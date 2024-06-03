using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInput : MonoBehaviour
{
    private bool buttonLeft, buttonRight;
    void Update()
    {
        // Ato de pressionar o botão:
        buttonLeft = Input.GetMouseButton(0); // 0 é botão esquerdo
        buttonRight = Input.GetMouseButton(1); // 1 é botão direito

        if (buttonLeft == true)
            print("Left button is actived");
        else
            print("Left button is deactivated");

        if (buttonRight == true)
            print("Right button is actived");
        else
            print("Right button is deactivated");

        // Ato de clicar no botão:
        buttonLeft = Input.GetMouseButtonDown(0); // 0 é botão esquerdo
        buttonRight = Input.GetMouseButtonDown(1); // 1 é botão direito

        if (buttonLeft == true)
            print("Left button was clicked");
        else
            print("Left button wasnt clicked");

        if (buttonRight == true)
            print("Right button was clicked");
        else
            print("Right button wasnt clicked");

        // Ato de clicar e soltar o botão:
        buttonLeft = Input.GetMouseButtonUp(0); // 0 é botão esquerdo
        buttonRight = Input.GetMouseButtonUp(1); // 1 é botão direito

        if (buttonLeft == true)
            print("Left button was loose");
        else
            print("Left button wasnt loose");

        if (buttonRight == true)
            print("Right button was loose");
        else
            print("Right button wasnt loose");

        if (Input.GetKey(KeyCode.Space))
            print("Spacebar was pressed");

        if (Input.GetKeyDown(KeyCode.Space))
            print("Spacebar was clicked");

        if (Input.GetKeyUp(KeyCode.Space))
            print("Spacebar was loose");
    } 
}
