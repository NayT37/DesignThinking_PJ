using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using UnityEngine.SceneManagement;

public class DataBaseParametersCtrl : MonoBehaviour {


	[Header ("Teacher logged in")]
	public Teacher _teacherLoggedIn;

	[Header ("Course loaded")]
	public Course _courseLoaded;

	[Header ("Group loaded")]
	public Group _groupLoaded;

	[Header ("Training loaded")]
	public Training _trainingloaded;

	[Header ("Case loaded")]
	public Case _caseLoaded;

	[Header ("Moment loaded")]
	public Moment _momentLoaded;

	[Header ("Project loaded")]
	public Project _projectLoaded;

	[Header ("Problem loaded")]
	public Problem _problemLoaded;

	[Header ("Field loaded")]
	public Field _fieldLoaded;

	[Header ("Public loaded")]
	public Public _publicLoaded;

	[Header ("StoryTelling loaded")]
	public StoryTelling _storyTellingLoaded;

	[Header ("Note loaded")]
	public Note _noteLoaded;

	[Header ("MindMap loaded")]
	public Mindmap _mindMapLoaded;

	[Header ("Section loaded")]
	public Section _sectionLoaded;

	[Header ("Node loaded")]
	public Node _nodeLoaded;


	[Header ("Evaluation loaded")]
	public Evaluation _evaluationLoaded;

	[Header ("Question loaded")]
	public Question _questionLoaded;

	[Header ("EmpathyMap loaded")]
	public Empathymap _empathyMapLoaded;

	[Header ("Sector loaded")]
	public Sector _sectorLoaded;
	public SQLiteConnection _sqliteConnection;
	public static DataBaseParametersCtrl Ctrl;

	public DataService _dataServices;

	public bool isWaitingToDB;

	public string _path;

	private string Salt;
    public string _ipServer;

	public bool isQueryOk;

	public IEnumerable<Course> _coursesLoaded;

	public IEnumerable<Group> _groupsLoaded;

	public bool isNotTeacherExist;


    void Awake () {
        if (Ctrl == null) {
            Ctrl = this;
			_ipServer = "http://emprendimientovalle.com/designBack/services/";
			isWaitingToDB = true;
			isNotTeacherExist = false;
			Salt = "EHS-dpa";
			_dataServices = new DataService ("dtdbtemplate.db");
			_sqliteConnection = _dataServices._connection;
			isQueryOk = false;
        } else if (Ctrl != null)
            Destroy (gameObject);
    }

	// Use this for initialization
	void Start () {

		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Method to obtain date and time at moment to update any query
	/// </summary>
	/// <returns>
	/// an string with the time and date with the next format (yyyy-mm-dd hh:mm:ss)
	/// </returns>
		
	public string GetDateTime(){

		DateTime theTime = System.DateTime.Now;
		string date = theTime.Year + "-" + theTime.Month + "-" + theTime.Day;
		string time = theTime.Hour + ":" + theTime.Minute + ":" + theTime.Second;

		string lastUpdateString = string.Format ("{0} {1}", date, time);

		return lastUpdateString;

	}

	public bool doConnection(){
		bool isConn = false;
		if(Application.internetReachability == NetworkReachability.NotReachable)
			isConn = false;
		else 
			isConn = true;
		
		return isConn;
	}

	
	public string GenerateSHA512String(string inputString)
    {
        HMACSHA512 hmacsha512 = new HMACSHA512 (Encoding.ASCII.GetBytes (Salt));
        hmacsha512.ComputeHash (Encoding.ASCII.GetBytes (inputString));
        return BitConverter.ToString (hmacsha512.Hash).Replace ("-", "").ToLower();
    }

	public Int64 GenerateCodeToId(){
		string teacherid = _teacherLoggedIn.identityCard;
        System.Random generator = new System.Random();
    	String r = teacherid + generator.Next(000000, 999999).ToString("D6");;
		return Convert.ToInt64(r);
	}

	public void sychMethod(){

		// var query = db.Table<Stock>().Where(s => s.Symbol.StartsWith("A"));

		// public static IEnumerable<Val> QueryVals (SQLiteConnection db, Stock stock) {
		// 	return db.Query<Val> ("select \"Price\" as \"Money\", \"Time\" as \"Date\" from Valuation where StockId = ?", stock.Id);
		// }

		//return db.Query<Val> ("select 'Price' as 'Money', 'Time' as 'Date' from Valuation where StockId = ?", stock.Id);​
	}



}