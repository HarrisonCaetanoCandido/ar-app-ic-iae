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
    public List<BarData> barData;
    // Capturo o asset prefab Bar
    public GameObject barPrefab;
    // Para definir comprimento, espaçamento e altura das barras
    public float barWidth;
    public float barSpacing;
    public float barHeightMultiplier;
    // Crio uma variável do tipo IObserver para poder receber atualizações de dados de diferentes APIs
    private ISubject consumeApi;
    // Dictionary armazena os pares label-value consumidos da API
    private Dictionary<string, float> data;
    // Tenho duas listas de GameObjects para fazer a atualização de valores após consumo pela API
    private List<GameObject> bars;
    private List<GameObject> labels;
    // Variável para pausa e despausa do gráfico
    private bool chartPause;
    // Permite ajustar o chart no marker
    private float gap;
    // Instancio o title do chart
    private GameObject chartTitle;
    // Instancio um componente de texto para a label
    private TextMesh chartTitleTextMesh;

    void Start()
    {
        // Essa é a única forma de instanciar a classe consumidora de API
        consumeApi = SingletonConsumeApi.GetInstance();
        consumeApi.RegisterObserver(this);
        barData = new List<BarData>();
        data = new Dictionary<string, float>();
        bars = new List<GameObject>();
        labels = new List<GameObject>();
        chartPause = false;
        gap = 0;
        chartTitle = new GameObject("Label");
        chartTitleTextMesh = chartTitle.AddComponent<TextMesh>();
        SetBarChart();
    }

    // O papel dessa função é capturar os game objects que representam barras no mundo e
    // destrui-los 
    public void DataUpdate(object o)
    {
        int i, n;

        // Defino comprimento, espaçamento e altura
        barWidth = 20.0f/1000f;
        barSpacing = 25.0f/1000f;
        barHeightMultiplier = 15.0f/1000f;

        CreateChartTitle(chartTitle, chartTitleTextMesh);

        // Se for falso então não está pausado
        if (!chartPause)
        {
            // Nomeio aquele texto com o nome da label
            chartTitleTextMesh.text = "BarChart está ativo";

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
        else
        {
            Debug.Log("BarChart is Paused");

            // Nomeio aquele texto com o nome da label
            chartTitleTextMesh.text = "BarChart está pausado";
        }
    }

    public void CreateChartTitle(GameObject label, TextMesh textMesh)
    {
        // Configuro a label para ser a terceira filha do GameObject BarChartTarget
        label.transform.SetParent(transform);
        // Defino a posição da label na barra
        label.transform.localPosition = new Vector3(gap / 2, 0.05f, 0.1f);
        // Ajusto a rotação para que o texto esteja sempre virado para a câmera
        label.transform.localEulerAngles = new Vector3(0, 0, 0);
        // Reescalo a label para aparecer maior na cena
        AutoSizeTextMesh(textMesh, 0.009f, 0.1f, 25f / 1000f, 5f / 1000f);

        // Centralizo o texto na label
        textMesh.anchor = TextAnchor.MiddleCenter;
        // Tamanho, peso e cor
        textMesh.color = Color.red;
        //textMesh.fontSize = 1;
        //textMesh.fontStyle = FontStyle.Bold;

        Debug.Log($"Label Created Locally: {textMesh.text} at local position {label.transform.localPosition}");
        Debug.Log($"Label Created Globally: {textMesh.text} at global position {label.transform.position}");

        Debug.Log($"Text Dimensions: Width={GetTextWidth(textMesh)} Heigth={GetTextHeight(textMesh)}");
        Debug.Log($"Text Font Size: {textMesh.fontSize}");
    }

    // Esse método permite controlar DataUpdate por um virtual button
    public void PauseChart()
    {
        if (!chartPause)
            chartPause = true;
        else
            chartPause = false;
    }

    private void SetBarChart()
    {
        int i;
        gap = 0;

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

        if (barData.Count > 1)
            gap = 2* GapCalculation();

        Debug.Log($"GAP: {gap}");

        // Observação, as instâncias de barPrefab são nomeadas como "NomeBarPrefab(Clone)"
        for (i = 0; i < barData.Count; i++)
        {
            BarData data = barData[i];

            // Instancio um prefab (molde) e posiciono como filho do BarChartTarget
            GameObject bar = Instantiate(barPrefab, transform);
            // Aplicamos uma transformação 3D de escala nas três dimensões da barra
            bar.transform.localScale = new Vector3(barWidth, data.value * barHeightMultiplier, barWidth);
            // Definimos a posição dos objetos no plano 3D
            bar.transform.localPosition = new Vector3(i * barSpacing - gap, data.value * barHeightMultiplier / 2.0f, 0);

            Debug.Log($"Bar Parent: {bar.transform.parent.name}");
            Debug.Log($"Bar Created Locally: {data.label} at local position {bar.transform.localPosition}");
            Debug.Log($"Bar Created Globally: {data.label} at global position {bar.transform.position}");

            // Instancio uma label
            GameObject label = new GameObject("Label");
            // Configuro a label para ser a segunda filha do GameObject BarChartTarget
            label.transform.SetParent(transform);
            // Defino a posição da label na barra
            label.transform.localPosition = new Vector3(i * barSpacing - gap, 5.0f/1000f, 5.0f/1000f);

            // Instancio um componente de texto para a label
            TextMesh textMesh = label.AddComponent<TextMesh>();
            // Nomeio aquele texto com o nome da label
            textMesh.text = data.label;
            // Centralizo o texto na label
            textMesh.anchor = TextAnchor.MiddleCenter;
            // Defino a cor do texto
            textMesh.color = Color.red;
            // Fonte bold
            textMesh.fontStyle = FontStyle.Bold;
            // As dimensões são definidas automaticamente com base num range
            // de tamanho (min, max) e width e height
            AutoSizeTextMesh(textMesh, 0.005f, 0.1f, 25f/1000f, 5f/1000f);

            Debug.Log($"Label Created Locally: {data.label} at local position {label.transform.localPosition}");
            Debug.Log($"Label Created Globally: {data.label} at global position {label.transform.position}");

            Debug.Log($"Text Dimensions: Width={GetTextWidth(textMesh)} Heigth={GetTextHeight(textMesh)}");
            Debug.Log($"Text Font Size: {textMesh.fontSize}");

            // Armazeno estes dados para excluir dps
            bars.Add(bar);
            labels.Add(label);
        }
    }

    // Gap existe para que nós possamos posicionar nosso gráfico de maneira centralizada no marker
    private float GapCalculation()
    {
        int nBars = barData.Count;
        int nSpaces = nBars - 1;
        float answer;

        answer = nBars * barWidth + nSpaces * barSpacing;

        return answer / (nBars + nSpaces);
    }

    private void AutoSizeTextMesh(TextMesh textMesh, float minScale, float maxScale, float labelWidth, float labelHeight)
    {
        Transform textTransform = textMesh.transform;

        for (float scale = maxScale; scale >= minScale; scale -= 0.01f)
        {
            // Defino a escala do TextMesh
            textTransform.localScale = new Vector3(scale, scale, scale);

            // Capturo as dimensões do texto
            float textWidth = GetTextWidth(textMesh);
            float textHeight = GetTextHeight(textMesh);

            // Verifico se o tamanho do texto ainda excede o tamanho máximo da label
            if (textWidth <= labelWidth && textHeight <= labelHeight)
                return;
        }

        // Se não encontro nenhum tamanho apropriado, defino a escala mínima
        textTransform.localScale = new Vector3(minScale, minScale, minScale);
    }

    private float GetTextWidth(TextMesh textMesh)
    {
        Renderer renderer = textMesh.GetComponent<Renderer>();
        return renderer.bounds.size.x;
    }

    private float GetTextHeight(TextMesh textMesh)
    {
        Renderer renderer = textMesh.GetComponent<Renderer>();
        return renderer.bounds.size.y;
    }
}
