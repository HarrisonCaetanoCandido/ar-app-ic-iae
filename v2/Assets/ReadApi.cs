using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private HttpWebRequest httpWebRequest;
    private HttpWebResponse httpWebResponse;
    private StreamReader streamReader;
    private string finalResponse;
    private Image bar1;
    private Image bar2;
    private Image bar3;
    private Image bar4;
    private Text txtBar1;
    private Text txtBar2;
    private Text txtBar3;
    private Text txtBar4;

    // Start is called before the first frame update
    void Start()
    {
        bar1 = GameObject.Find("bar1").GetComponent<Image>();
        bar2 = GameObject.Find("bar2").GetComponent<Image>();
        bar3 = GameObject.Find("bar3").GetComponent<Image>();
        bar4 = GameObject.Find("bar4").GetComponent<Image>();

        txtBar1 = GameObject.Find("txtBar1").GetComponent<Text>();
        txtBar2 = GameObject.Find("txtBar2").GetComponent<Text>();
        txtBar3 = GameObject.Find("txtBar3").GetComponent<Text>();
        txtBar4 = GameObject.Find("txtBar4").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float[] res = new float[4];
        res = data();
        print("Data 1: " + res[0] + " Data 2: " + res[1] + " Data 3: " + res[2] + " Data 4: " + res[3]);

        bar1.transform.localScale = new Vector3(1, res[0]/10, 1);
        bar2.transform.localScale = new Vector3(1, res[1]/10, 1);
        bar3.transform.localScale = new Vector3(1, res[2]/10, 1);
        bar4.transform.localScale = new Vector3(1, res[3]/10, 1);

        txtBar1.text = res[0].ToString();
        txtBar2.text = res[1].ToString();
        txtBar3.text = res[2].ToString();
        txtBar4.text = res[3].ToString();
    }

    public float[] data()
    {
        string[] r;
        float[] r2;

        httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://f6ddb3e8-1075-4fb3-a7f6-bbeb1b7e045a-00-32kcm5u02c8d2.worf.replit.dev/"));
        httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        streamReader = new StreamReader(httpWebResponse.GetResponseStream());
        finalResponse = streamReader.ReadToEnd();
        r = finalResponse.Split(',');
        r2 = new float[4];
        r2[0] = float.Parse(r[0])/10;
        r2[1] = float.Parse(r[1])/10;
        r2[2] = float.Parse(r[2])/10;
        r2[3] = float.Parse(r[3])/10;

        return r2;
    }
}
