﻿using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	public SQLiteConnection _connection;
	private SQLiteException _exception;

	private bool _isFirstTime = false;

	public DataService(string DatabaseName){

		#if UNITY_EDITOR
				var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

			#if UNITY_ANDROID 
						var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
						while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
						// then save to Application.persistentDataPath
						File.WriteAllBytes(filepath, loadDb.bytes);
						_isFirstTime = true;

			#elif UNITY_IOS
							var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
							// then save to Application.persistentDataPath
							File.Copy(loadDb, filepath);
			#elif UNITY_WP8
							var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in WindowsPhone
							// then save to Application.persistentDataPath
							File.Copy(loadDb, filepath);

			#elif UNITY_WINRT
					var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in ---
					// then save to Application.persistentDataPath
					File.Copy(loadDb, filepath);
					
			#elif UNITY_STANDALONE_OSX
					var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in OSX
					// then save to Application.persistentDataPath
					File.Copy(loadDb, filepath);
			#else
				var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in PC
				
				string result;
				if (loadDb.Contains("://") || loadDb.Contains(":///"))
				{
					WWW www = new WWW(loadDb);
					yield return www;
					result = www.text;
				}
				// then save to Application.persistentDataPath
				File.Copy(result, filepath);

			#endif

					Debug.Log("Database written");

			}

					var dbPath = filepath;
		#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath); 

	}
	
}
