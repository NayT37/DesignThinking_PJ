using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelImage : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private Button _updateBtn, _detBtn, _closeBtn;
    private RawImage _internalImg;
    private Ctrl_M4 _ctrl;
    [SerializeField]
    private Texture _defaultTexture;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _updateBtn = transform.Find("ButtonsHolder").Find("UpdateBtn").GetComponent<Button>();
        _detBtn = transform.Find("ButtonsHolder").Find("DeleteBtn").GetComponent<Button>();
        _internalImg = transform.Find("RawImage").GetComponent<RawImage>();
        _closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.AddListener(ClosePanel);
        _internalImg.texture = _defaultTexture;
    }

    public void UpdateInternalImg()
    {
        if (!_ctrl)
        {
            _ctrl = GameObject.FindObjectOfType<Ctrl_M4>();
        }
        //_ctrl.UpdateImgFromDB();
        //Local changes here
    }

    public void DeleteInternalImg()
    {
        if (!_ctrl)
        {
            _ctrl = GameObject.FindObjectOfType<Ctrl_M4>();
        }
        _ctrl.DeleteImgFromDB();
        //Local changes here
        _internalImg.texture = null;
    }

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public void SetInternalImg(Texture img)
    {
        if (img == null)
        {
            _detBtn.gameObject.SetActive(false);
            _internalImg.texture = _defaultTexture;
        }
        else
        {
            _internalImg.texture = img;
        }
    }
    #endregion


    #region COROUTINES
    #endregion
}
