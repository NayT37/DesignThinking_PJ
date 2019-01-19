using SQLite4Unity3d;
using System;

[Serializable]
public class Empathymap  {

	// [PrimaryKey, AutoIncrement]
	// public int id { get; set; }
	// public int percentage { get; set; }
	// public string creationDate { get; set; }

	// public int projectId { get; set; }
	// public string lastUpdate { get; set; }

	public int id;
	public int percentage;
	public string creationDate;

	public int projectId;
	public string lastUpdate;
	

	public override string ToString ()
	{
		return string.Format ("[EmpathyMap: Id={0},  percentage={1}, creationDate={2}, projectId={3}, lastUpdate={4}]", 
								id, percentage, creationDate, projectId, lastUpdate);
	}

	
}
