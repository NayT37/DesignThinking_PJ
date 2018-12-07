using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaControl : MonoBehaviour
{

    private bool vuforiaCtrl;
    private Camera _default, _arCam;

    void Awake()
    {
        DontDestroyOnLoad(this);
        _default = GameObject.Find("DefaultCamera").GetComponent<Camera>();
        _arCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Start()
    {
        /*         vuforiaCtrl = false;
                VuforiaBehaviour.Instance.enabled = vuforiaCtrl;
                _default.gameObject.SetActive(false); */
        StartCoroutine(WaitTime());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_default.gameObject.activeInHierarchy) { _default.gameObject.SetActive(true); _arCam.gameObject.SetActive(false); }
            vuforiaCtrl = !vuforiaCtrl;
            _default.gameObject.SetActive(!vuforiaCtrl);
            _arCam.gameObject.SetActive(vuforiaCtrl);
            //    VuforiaBehaviour.Instance.enabled = vuforiaCtrl;
        }
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        //  _default.gameObject.SetActive(true);
        //   _arCam.gameObject.SetActive(false);
    }

}
