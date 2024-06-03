using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Prefs é uma maneira fácil de salvar dados do tipo string, int e float
// Criado para salvar preferencias do jogador (idioma, brilho, cor etc)
// Problema: Player Prefs são vulneráveis por serem fáceis de alterar por um usuário mal intencionado
public class UnityPlayerPrefs : MonoBehaviour
{
    int value;

    void Start()
    {
        value = 10;
        print(value);
        PlayerPrefs.SetInt("value", value);
        PlayerPrefs.Save();

        print(PlayerPrefs.GetInt("value"));

        // Verifico se aquela chave de acesso existe no jogo
        print(PlayerPrefs.HasKey("value"));
    }
}
