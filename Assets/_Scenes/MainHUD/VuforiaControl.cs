using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

using System;

public class VuforiaControl : MonoBehaviour
{
    private Camera _mainCam, _defaultCam;
    private AudioListener _mainCamAudio;
    public static VuforiaControl instance = null;
    private bool _isActiveRA;
    private EvaluateHolder _evaluateHolder;
    private Transform _testMarker;
    private Vector3 tempPos, tempSize;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _defaultCam = GameObject.Find("DefaultCamera").GetComponent<Camera>();
        _mainCamAudio = _mainCam.gameObject.GetComponent<AudioListener>();
        _mainCamAudio.enabled = false;
        _isActiveRA = false;
        _testMarker = GameObject.Find("TestMarker").GetComponent<Transform>();
        StartCoroutine(WaitTime());
    }


    public void ChangeRAStatus()
    {
        _isActiveRA = !_isActiveRA;
        _evaluateHolder = GameObject.FindObjectOfType<EvaluateHolder>();
        if (_isActiveRA)
        {
            tempPos = _evaluateHolder.transform.localPosition;
            tempSize = _evaluateHolder.transform.localScale;
            _evaluateHolder.transform.localPosition = new Vector3(0, 0, 0);
            _mainCamAudio.enabled = true;
            _mainCam.transform.localPosition = new Vector3(-800, -15, 0);
            _defaultCam.gameObject.SetActive(false);
            _evaluateHolder.SetNewParent(_testMarker);
            _evaluateHolder.transform.localPosition = new Vector3(0, 0, 0);
            _evaluateHolder.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            var rendererComponents = _evaluateHolder.GetComponentsInChildren<Renderer>(true);
            foreach (var component in rendererComponents)
            {
                component.enabled = false;
            }
        }
        else
        {
            _mainCamAudio.enabled = false;
            _defaultCam.gameObject.SetActive(true);
            _evaluateHolder.SetNewParent(null);
            _evaluateHolder.transform.localPosition = tempPos;
            _evaluateHolder.transform.localScale = tempSize;
            var rendererComponents = _evaluateHolder.GetComponentsInChildren<Renderer>(true);
            foreach (var component in rendererComponents)
            {
                component.enabled = true;
            }
        }
    }

    public void RestartModelPos()
    {
        try
        {
            _mainCamAudio.enabled = false;
            _defaultCam.gameObject.SetActive(true);
            _evaluateHolder.SetNewParent(null);
            _evaluateHolder.transform.localPosition = tempPos;
            _evaluateHolder.transform.localScale = tempSize;
            XRSettings.enabled = false;
        }
        catch (Exception e) { }
    }

    public void ResetRA()
    {
        _isActiveRA = false;
        _mainCamAudio.enabled = false;
        _defaultCam.gameObject.SetActive(true);
        _mainCam.transform.localPosition = new Vector3(-800, -15, 0);
    }


    public bool GetActiveStatus()
    {
        return _isActiveRA;
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        try { _mainCam.transform.localPosition = new Vector3(-800, -15, 0); }
        catch (Exception e)
        {
            _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
            _mainCam.transform.localPosition = new Vector3(-800, -15, 0);
        }
    }

}
