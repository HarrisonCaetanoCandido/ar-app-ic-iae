using UnityEngine;
using Vuforia;

public class KnowMore : MonoBehaviour
{
    private GameObject secInfoCardPanel;
    private GameObject tercInfoCardPanel;

    // Start is called before the first frame update
    void Start()
    {
        secInfoCardPanel = GameObject.Find("SecInfoCardPanel");
        tercInfoCardPanel = GameObject.Find("TercInfoCardPanel");

        if (secInfoCardPanel != null && tercInfoCardPanel != null)
        {
            Debug.Log("Sec Panel encontrado. Terc Panel encontrado");
            OnButtonClick();
        }
        else
            Debug.Log("Sec Panel não encontrado. Terc Panel não encontrado");
    }

    // O método que vai ser chamado pelo unity nao pode ser privado
    public void OnButtonClick()
    {
        Change(secInfoCardPanel);
        Change(tercInfoCardPanel);
    }

    // Consigo comutar o estado dos panels
    private void Change(GameObject panel)
    {
        bool isActive = panel.activeSelf;
        panel.SetActive(!isActive);
    }
}
