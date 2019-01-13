using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : MonoBehaviour, IPointerDownHandler {
    public RawImage output;

    private Texture2D _texture2DRaw;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        var extensions = new [] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };
        UploadFile(gameObject.name, "OnFileUpload", extensions, false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start() {
        _texture2DRaw = new Texture2D(1,1);
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        var extensions = new [] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", extensions, false);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
#endif

    private IEnumerator OutputRoutine(string url) {
        var loader = new WWW(url);
        yield return loader;
        output.texture = loader.texture;
        output.SetNativeSize();
        output.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
        _texture2DRaw = (Texture2D)output.texture;
        convertToBase64(_texture2DRaw);
    }

    private void convertToBase64(Texture2D _Texture){

        byte[] imageData = _Texture.EncodeToPNG();
		string data = Convert.ToBase64String(imageData);
        Debug.Log(data);
    }
}