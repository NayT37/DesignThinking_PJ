using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using SimpleFileBrowser;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : MonoBehaviour {
    public RawImage output;

    private Texture2D _texture2DRaw;
 
    private Ctrl_M4 ctrl;

    //
    // Standalone platforms & editor
    //
    
    void Start() {
        ctrl = GameObject.Find("Ctrl_M4").GetComponent<Ctrl_M4>();

        _texture2DRaw = new Texture2D(1,1);
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        // Set filters (optional)
		// It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
		// if all the dialogs will be using the same filters
		FileBrowser.SetFilters( true, new FileBrowser.Filter( "Images", ".jpg", ".png" ));

		// Set default filter that is selected when the dialog is shown (optional)
		// Returns true if the default filter is set successfully
		// In this case, set Images filter as the default filter
		FileBrowser.SetDefaultFilter( ".jpg" );

		// Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
		// Note that when you use this function, .lnk and .tmp extensions will no longer be
		// excluded unless you explicitly add them as parameters to the function
		FileBrowser.SetExcludedExtensions( ".lnk", ".tmp", ".zip", ".rar" );

		// Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
		// It is sufficient to add a quick link just once
		// Name: Users
		// Path: C:\Users
		// Icon: default (folder icon)
		FileBrowser.AddQuickLink( "Users", "C:\\Users", null );

		// Show a save file dialog 
		// onSuccess event: not registered (which means this dialog is pretty useless)
		// onCancel event: not registered
		// Save file/folder: file, Initial path: "C:\", Title: "Save As", submit button text: "Save"
		// FileBrowser.ShowSaveDialog( null, null, false, "C:\\", "Save As", "Save" );

		// Show a select folder dialog 
		// onSuccess event: print the selected folder's path
		// onCancel event: print "Canceled"
		// Load file/folder: folder, Initial path: default (Documents), Title: "Select Folder", submit button text: "Select"
		// FileBrowser.ShowLoadDialog( (path) => { Debug.Log( "Selected: " + path ); }, 
		//                                () => { Debug.Log( "Canceled" ); }, 
		//                                true, null, "Select Folder", "Select" );
    }

    private void OnClick() {
        StartCoroutine( ShowLoadDialogCoroutine() );
    }

    private IEnumerator ShowLoadDialogCoroutine() {
        // Show a load file dialog and wait for a response from user
		// Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog( false, null, "Seleccionar imagen para prototipo", "Cargar" );

		// Dialog is closed
		// Print whether a file is chosen (FileBrowser.Success)
		// and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
		Debug.Log( FileBrowser.Success + " " + FileBrowser.Result);
        
        
		if (FileBrowser.Success)
		{
			var loader = new WWW(FileBrowser.Result);
        	yield return loader;
			output.texture = loader.texture;
			//output.SetNativeSize();
			//output.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
			_texture2DRaw = (Texture2D)output.texture;
			string imageConvert = convertToBase64(_texture2DRaw);
			//ctrl.UpdateImgFromDB(imageConvert);
		}
        
    }

    private string convertToBase64(Texture2D _Texture){

        byte[] imageData = _Texture.EncodeToPNG();
		string data = Convert.ToBase64String(imageData);
        Debug.Log(data);
        return data;
    }
}