using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System.Collections;

public class SingletonConsumeApi : MonoBehaviour, ISubject
{
    private HttpWebRequest httpWebRequest;
    private HttpWebResponse httpWebResponse;
    private StreamReader streamReader;
    private string finalResponse;
    private static SingletonConsumeApi instance = null;
    private List<IObserver> observers = new List<IObserver>();
    private Dictionary<string, float> data = new Dictionary<string, float>();

    // Objetos Unity são instanciados automaticamente pelo gameObject associado,
    // então esse construtor não faz diferença estar privado ou não, mas se 
    // encontra aqui por convenção
    private SingletonConsumeApi()
    { 
    }

    // Aqui fica definida a única forma de instanciar a API, garantindo que só exista uma na cena
    public static SingletonConsumeApi GetInstance()
    {
        // Procuro um objeto desse na cena. Se associado a um gameObject, ele é automaticamente instanciado, por
        // isso fazemos essa busca
        instance = FindObjectOfType<SingletonConsumeApi>();

        //  Se ele não existir, instancio um novo
        if (instance == null)
        {
            // Crio um novo gameObject
            GameObject singletonObject = new GameObject("SingletonConsumeApi");
            // Adiciono o objeto no gameObject
            instance = singletonObject.AddComponent<SingletonConsumeApi>();
            DontDestroyOnLoad(singletonObject);
        }
        return instance;
    }

    void Start()
    {
        StartCoroutine(UpdateWithDelay(1f));
    }

    // Update is called once per frame
    void Update()
    {
        // Se a gente atrasa a atualização do update aqui dentro, podemos ter problemas de desempenho
    }

    // Corrotina que substitui void Update
    private IEnumerator UpdateWithDelay(float delay)
    {
        while (true)
        {
            // O método abaixo atualiza o dicionário data
            Data();

            foreach (KeyValuePair<string, float> pair in data)
                print(pair.Key + ": " + pair.Value);

            print("OBSERVERS: " + observers.Count);

            NotifyObservers();

            yield return new WaitForSeconds(delay);
        }
    }

    public void RegisterObserver(IObserver o)
    {
        observers.Add(o);
    }

    public void RemoveObserver(IObserver o)
    {
        observers.Remove(o);
    }

    public void NotifyObservers()
    {
        int i;
        for (i = 0; i < observers.Count; i++)
            observers[i].DataUpdate(data);
    }

    private void Data()
    {
        string[] r;
        int i, n;

        // Capturo os dados
        httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://f6ddb3e8-1075-4fb3-a7f6-bbeb1b7e045a-00-32kcm5u02c8d2.worf.replit.dev/"));
        httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        streamReader = new StreamReader(httpWebResponse.GetResponseStream());
        finalResponse = streamReader.ReadToEnd();

        // Trato os dados
        r = finalResponse.Split(',');
        n = r.Length;
        
        // Sabemos que as posições ímpares possuem as chaves e as pares os valores, sendo,
        // a partir da primeira posição o par key-value, então temos o seguinte
        for(i = 0; i < n; i = i + 2)
            data[r[i]] = float.Parse(r[i + 1]) / 10;
    }
}
