using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCtrl : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public GameObject quadObj;
    private CasesHUD_Ctrl casesCtrl;
    private GameObject closeBtn, openBtn;

    // Use this for initialization
    void Start()
    {
        closeBtn = GameObject.Find("BtnClose");
        openBtn = GameObject.Find("BtnPlay");
        closeBtn.SetActive(false);
        casesCtrl = FindObjectOfType<CasesHUD_Ctrl>();
        quadObj.SetActive(false);
    }

    public void StartVideo()
    {
        quadObj.SetActive(true);
        videoPlayer.Play();
        closeBtn.SetActive(true);
        openBtn.SetActive(false);
    }
    public void CloseVideo()
    {
        videoPlayer.Stop();
        quadObj.SetActive(false);
        closeBtn.SetActive(false);
        openBtn.SetActive(true);
    }

}
