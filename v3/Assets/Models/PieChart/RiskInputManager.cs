using ChancePoolExamples;
using UnityEngine;
using TMPro;

public class RiskInputManager : MonoBehaviour
{
    public ChancePoolExampleManager exampleManager;
    public GameObject bemSucedidoInput;
    public GameObject falhaTecnicaInput;
    public GameObject problemaMenorInput;
    public GameObject falhaCriticaInput;

    void Start()
    {
        bemSucedidoInput = GameObject.Find("SInput");
        falhaTecnicaInput = GameObject.Find("TInput");
        problemaMenorInput = GameObject.Find("LInput");
        falhaCriticaInput = GameObject.Find("CInput");

        // Verificando se todos os GameObjects foram encontrados
        if (bemSucedidoInput != null) Debug.Log("SInput GameObject found!");
        if (falhaTecnicaInput != null) Debug.Log("TInput GameObject found!");
        if (problemaMenorInput != null) Debug.Log("LInput GameObject found!");
        if (falhaCriticaInput != null) Debug.Log("CInput GameObject found!");

        UpdateRiskPool();
    }

    public void UpdateRiskPool()
    {
        // Tentando parsear os valores dos InputFields
        float bemSucedidoValue = ParseInputField(bemSucedidoInput.GetComponent<TMP_InputField>());
        float falhaTecnicaValue = ParseInputField(falhaTecnicaInput.GetComponent<TMP_InputField>());
        float problemaMenorValue = ParseInputField(problemaMenorInput.GetComponent<TMP_InputField>());
        float falhaCriticaValue = ParseInputField(falhaCriticaInput.GetComponent<TMP_InputField>());

        Debug.Log("Bem Sucedido: " + bemSucedidoValue);
        Debug.Log("Falha Técnica: " + falhaTecnicaValue);
        Debug.Log("Problema Menor: " + problemaMenorValue);
        Debug.Log("Falha Crítica: " + falhaCriticaValue);


        // Definindo os valores no riskPool
        exampleManager.riskPool.SetChance(RocketLaunchRisk.BemSucedido, bemSucedidoValue);
        exampleManager.riskPool.SetChance(RocketLaunchRisk.FalhaTecnica, falhaTecnicaValue);
        exampleManager.riskPool.SetChance(RocketLaunchRisk.ProblemaMenor, problemaMenorValue);
        exampleManager.riskPool.SetChance(RocketLaunchRisk.FalhaCritica, falhaCriticaValue);

        exampleManager.riskPool.RedrawVisualizer();
    }

    private float ParseInputField(TMP_InputField inputField)
    {
        if (inputField == null)
        {
            Debug.LogError("InputField component is missing!");
            return 0f;
        }

        string inputText = inputField.text;

        if (string.IsNullOrEmpty(inputText))
        {
            Debug.LogWarning("InputField is empty, defaulting to 0.");
            return 0f;
        }

        if (float.TryParse(inputText, out float value))
        {
            return value;
        }
        else
        {
            Debug.LogError("Invalid float value in InputField, defaulting to 0.");
            return 0f;
        }
    }
}
