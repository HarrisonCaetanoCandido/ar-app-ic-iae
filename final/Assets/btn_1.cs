using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class btn_1 : MonoBehaviour
{
    private VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
        video = GameObject.Find("video").GetComponent<VideoPlayer>();
        this.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(acao_btn1);
    }

    public void acao_btn1(VirtualButtonBehaviour vb)
    {
        if (video.isPlaying == true)
            video.Stop();
        else
            video.Play();
    }

}
