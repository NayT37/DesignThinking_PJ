using SQLite4Unity3d;
using System;
[Serializable]
public class Case  {

	// [PrimaryKey, AutoIncrement]
	// public int id;
	// public string name;
	// public int percentage;
	// public string creationDate;

	// public int trainingId;
	// public string lastUpdate;

	public int id;
	public string name;
	public int percentage;
	public string creationDate;

	public int trainingId;
	public string lastUpdate;

	public override string ToString ()
	{
		return string.Format ("[Case: Id={0}, Name={1},  percentage={2}, creationDate={3}, trainingId={4}, lastUpdate={5}]", 
								id, name, percentage, creationDate, trainingId, lastUpdate);
	}

	
}
