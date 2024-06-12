using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Serializar a struct permite que ela apareça no Inspector
[System.Serializable]
public struct BarData
{
    public string label;
    public float value;

    public BarData(string label, float value)
    {
        this.label = label;
        this.value = value;
    }
}

public class BarChart : MonoBehaviour, IObserver
{
    // Cada nó da lista corresponde a uma barra de dados com label e valor
    public List<BarData> barData = new List<BarData>();
    // Capturo o asset prefab Bar 
    public GameObject barPrefab;
    // Defino comprimento, espaçamento e altura das barras
    public float barWidth = 10.0f;
    public float barSpacing = 10.5f;
    public float barHeightMultiplier = 2.0f;
    // Crio uma variável do tipo IObserver para poder receber atualizações de dados de diferentes APIs
    private ISubject consumeApi;
    // Dictionary armazena os pares label-value consumidos da API
    private Dictionary<string, float> data = new Dictionary<string, float>();
    // Tenho duas listas de GameObjects para fazer a atualização de valores após consumo pela API
    private List<GameObject> bars = new List<GameObject>();
    private List<GameObject> labels = new List<GameObject>();

    void Start()
    {
        // Essa é a única forma de instanciar a classe consumidora de API
        consumeApi = SingletonConsumeApi.GetInstance();
        consumeApi.RegisterObserver(this);
        SetBarChart();
    }

    // O papel dessa função é capturar os game objects que representam barras no mundo e
    // destrui-los 
    public void DataUpdate(object o)
    {
        int i, n;

        print("Bar data: " + barData.Count);
        Dictionary<string, float> d = (Dictionary<string, float>)o;

        i = 0;
        n = bars.Count;
        // Tanto a lista bars, quanto a lista labels possuem a mesma quantidade de nós
        if (bars.Any())
        {
            while (i < n)
            {
                DestroyImmediate(bars[i]);
                DestroyImmediate(labels[i]);
                i++;
            }

            bars.Clear();
            labels.Clear();
            data.Clear();
        }
        data = d;
        barData.Clear();
        SetBarChart();
    }

    private void SetBarChart()
    {
        int i;
        // Inicializo as barras de acordo com as labels e valores consumidos da API.
        // Se não houverem dados a serem consumidos ou a api estiver offline, o laço
        // não será iterado
        if (data.Any())
            foreach (KeyValuePair<string, float> pair in data)
            {
                BarData newBar;
                newBar.label = pair.Key;
                newBar.value = pair.Value;

                barData.Add(newBar);
            }

        // Observação, as instâncias de barPrefab são nomeadas como "NomeBarPrefab(Clone)"
        for (i = 0; i < barData.Count; i++)
        {
            BarData data = barData[i];

            // Instancio um prefab (molde) e posiciona como filho do transform atual (componente que define posição, rotação e escala do objeto no plano 3D)
            GameObject bar = Instantiate(barPrefab, transform);
            // Aplicamos uma transformação 3D de escala nas três dimensões da barra
            bar.transform.localScale = new Vector3(barWidth, data.value * barHeightMultiplier, barWidth);
            // Definimos a posição dos objetos no plano 3D
            bar.transform.localPosition = new Vector3(i * barSpacing, data.value * barHeightMultiplier / 2.0f, 0);

            // Instancio um objeto label para nomear a barra
            GameObject label = new GameObject("Label");
            // Configuro a label para esta exata barra
            label.transform.SetParent(bar.transform);
            // Instancio um componente de texto para a label
            TextMesh textMesh = label.AddComponent<TextMesh>();
            // Nomeio aquele texto com o nome da label
            textMesh.text = data.label;
            // Centralizo o texto na label
            textMesh.anchor = TextAnchor.MiddleCenter;
            // Defino a posição da label na barra
            label.transform.localPosition = new Vector3(0, data.value * barHeightMultiplier / 2.0f + 1, 0);

            // Armazeno estes dados para excluir dps
            bars.Add(bar);
            labels.Add(label);
        }
    }
}