using SQLite4Unity3d;
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

	/// <summary>
	/// Description to method to Insert many groups
	/// </summary>
	/// <param name="date">
	/// attribute that contains time and date to insert in the fields "creationDate" and "lastUpdate".
	/// </param>
	/// <returns>
	/// an arrangement that contains all the groups that were successfully inserted.
	/// </returns>

	private void createTables(){

		_connection.CreateTable<Zone>();
		_connection.CreateTable<Town>();
		_connection.CreateTable<School>();
		_connection.CreateTable<Headquarters>();
		_connection.CreateTable<Teacher>();
		_connection.CreateTable<DocumentType>();
		_connection.CreateTable<Teacher>();
		_connection.CreateTable<Course>();
		_connection.CreateTable<Group>();
		_connection.CreateTable<Training>();
		_connection.CreateTable<Case>();
		_connection.CreateTable<Moment>();
		_connection.CreateTable<Project>();
		_connection.CreateTable<Public>();
		_connection.CreateTable<Problem>();
		_connection.CreateTable<Field>();
		_connection.CreateTable<Empathymap>();
		_connection.CreateTable<Section>();
		_connection.CreateTable<StoryTelling>();
		_connection.CreateTable<Mindmap>();		
		_connection.CreateTable<Evaluation>();
		_connection.CreateTable<Question>();
		_connection.CreateTable<Answer>();
		_connection.CreateTable<Section>();
		_connection.CreateTable<Node>();

	}
	public void InsertGroups(string date){
		

		_connection.InsertAll (new[]{
			new Group{
				name = "Jojoa-Team1",
				creationDate = date,
				studentsCounter = 5,
				courseId = 1,
				lastUpdate = date,
			},
			new Group{
				name = "Jojoa-Team2",
				creationDate = date,
				studentsCounter = 5,
				courseId = 1,
				lastUpdate = date,
			},
			new Group{
				name = "Jojoa-Team3",
				creationDate = date,
				studentsCounter = 5,
				courseId = 1,
				lastUpdate = date,
			}
		});

		//Debug.Log(_exception);
	}

	/// <summary>
	/// Description to method get all people
	/// </summary>
	/// <param name="typeOfSearch">
	/// Integer to define type of search.
	/// 1: 
	/// 2:
	/// 3:
	/// 4: 
	/// </param>
	/// <param name="param1">
	/// Ohet parameter
	/// </param>
	/// <param name="param2">
	/// Other parameter
	/// </param>
	/// <returns>
	/// An Object of the Person class.!--.!--.
	/// </returns>
	public IEnumerable<Person> GetPersons(){
		return _connection.Table<Person>();
	}

	public IEnumerable<Headquarters> GetHeadquarters(){
		return _connection.Table<Headquarters>();
	}


	/// <summary>
	/// Description to method Get Persons with the same attribute
	/// </summary>
	/// <param name="typeOfSearch">
	/// Integer to define type of search.
	/// 1: 
	/// 2:
	/// 3:
	/// 4: 
	/// </param>
	/// <param name="param1">
	/// Ohet parameter
	/// </param>
	/// <param name="param2">
	/// Other parameter
	/// </param>
	/// <returns>
	/// An Object of the Person class.!--.!--.
	/// </returns>
	public IEnumerable<Person> GetPersonsNamed(){
		return _connection.Table<Person>().Where(x => x.Name == "Roberto");
	}

	/// <summary>
	/// Description to method Delete Person
	/// </summary>
	/// <param name="typeOfSearch">
	/// Integer to define type of search.
	/// 1: 
	/// 2:
	/// 3:
	/// 4: 
	/// </param>
	/// <param name="param1">
	/// Ohet parameter
	/// </param>
	/// <param name="param2">
	/// Other parameter
	/// </param>
	/// <returns>
	/// An Object of the Person class.!--.!--.
	/// </returns>
	public int DeletePerson(string personName){
		return _connection.Delete<Person>(personName);
	}

	public int UpdatePerson(){
		return _connection.Update(
				new Person{
				Id = 10,
				Name = "Stuart",
				Surname = "Huertas Quiñonez",
				Age = 68
			});
	}

	/// <summary>
	/// Description to method Get Person
	/// </summary>
	/// <param name="typeOfSearch">
	/// Integer to define type of search.
	/// 1: 
	/// 2:
	/// 3:
	/// 4: 
	/// </param>
	/// <param name="param1">
	/// Ohet parameter
	/// </param>
	/// <param name="param2">
	/// Other parameter
	/// </param>
	/// <returns>
	/// An Object of the Person class.!--.!--.
	/// </returns>
	public Person GetPerson(int typeOfSearch, string param1, string param2){

		var result = new Person();
		switch(typeOfSearch){

			case 1:
				int value = Convert.ToInt32(param1);
				result = _connection.Table<Person>().Where(x => x.Id == value).FirstOrDefault();
			break;

			case 2:
				result = _connection.Table<Person>().Where(x => x.Name == param1).FirstOrDefault();
			break;

			case 3:
				result = _connection.Table<Person>().Where(x => x.Surname == param2).FirstOrDefault();
			break;

			case 4:
				result = _connection.Table<Person>().Where((x => x.Name == param1)).Where((x => x.Surname == param2)).Where((x => x.Age == 39)).FirstOrDefault();
			break;

			default:
				result = new Person{
				Name = "",
				Surname = "",
				Age = 0
				};
			break;
		}

		return result;
		
	}

	public Person CreatePerson(){
		var p = new Person{
				Name = "Johnny",
				Surname = "Mnemonic",
				Age = 21
		};
		_connection.Insert (p);
		return p;
	}

	public Group CreateGroup(string date){
		var p = new Group{
				name = "Jojoa-Team2",
				creationDate = date,
				studentsCounter = 5,
				courseId = 15,
				lastUpdate = date,
		};
		_connection.Insert (p);
		return p;
	}

	/// <summary>
	/// Description to method Get Persons with the same attribute
	/// </summary>
	/// <param name="typeOfSearch">
	/// Integer to define type of search.
	/// 1: 
	/// 2:
	/// 3:
	/// 4: 
	/// </param>
	/// <param name="param1">
	/// Ohet parameter
	/// </param>
	/// <param name="param2">
	/// Other parameter
	/// </param>
	/// <returns>
	/// An Object of the Person class.!--.!--.
	/// </returns>
	public IEnumerable<Course> GetCourseNamed(string courseName){
		return _connection.Table<Course>().Where(x => x.name == courseName);
	}

	/// <summary>
	/// Description to method get all people
	/// </summary>
	/// <param name="typeOfSearch">
	/// Integer to define type of search.
	/// 1: 
	/// 2:
	/// 3:
	/// 4: 
	/// </param>
	/// <param name="param1">
	/// Ohet parameter
	/// </param>
	/// <param name="param2">
	/// Other parameter
	/// </param>
	/// <returns>
	/// An Object of the Person class.!--.!--.
	/// </returns>
	public IEnumerable<Course> GetCourses(){
		return _connection.Table<Course>();
	}
}
