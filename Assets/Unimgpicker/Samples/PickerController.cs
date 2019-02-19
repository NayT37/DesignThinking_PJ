using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Kakera
{
    public class PickerController : MonoBehaviour
    {
        [SerializeField]
        private Unimgpicker imagePicker;

        [SerializeField]
        private RawImage image;

        private Texture2D _texture2DRaw;

        private Ctrl_M4 ctrl;

        void Awake()
        {
            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path));
            };
        }

        void Start(){
            _texture2DRaw = new Texture2D(1,1);
            ctrl = GameObject.Find("Ctrl_M4").GetComponent<Ctrl_M4>();
        }

        public void OnPressShowPicker()
        {
            imagePicker.Show("Seleccionar imagen de prototipo", "unimgpicker", 1024);
        }

        private IEnumerator LoadImage(string path)
        {
            var url = "file://" + path;
            var www = new WWW(url);
            yield return www;

            var texture = www.texture;
			if (texture == null) {
				Debug.LogError ("Failed to load texture url:" + url);
			}
            image.texture = texture;
			//output.SetNativeSize();
			//output.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
			_texture2DRaw = (Texture2D)texture;
			string imageConvert = convertToBase64(_texture2DRaw);
			ctrl.UpdateImgFromDB(imageConvert);
        }

        private string convertToBase64(Texture2D _Texture){

        byte[] imageData = _Texture.EncodeToPNG();
		string data = Convert.ToBase64String(imageData);
        Debug.Log(data);
        return data;
    }
    }
}