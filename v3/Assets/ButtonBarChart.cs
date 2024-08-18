using UnityEngine;
using Vuforia;

public class ButtonBarChart : MonoBehaviour
{
    private BarChart barChart;

    // Start is called before the first frame update
    void Start()
    {
        barChart = FindObjectOfType<BarChart>();
        this.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(Action);
    }

    public void Action(VirtualButtonBehaviour vb)
    {
        barChart.PauseChart();
    }

}
