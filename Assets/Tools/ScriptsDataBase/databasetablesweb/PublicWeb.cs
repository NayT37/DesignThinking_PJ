using SQLite4Unity3d;
using System;

[Serializable]
public class PublicWeb  {

	// [PrimaryKey, AutoIncrement]
	// public int id { get; set; }
	// public string ageRange { get; set; }

	// public string gender { get; set; }
	// public int percentage { get; set; }
	// public string creationDate { get; set; }

	// public string lastUpdate { get; set; }

	// public int projectId { get; set; }
	public Int64 id;
	public string ageRange;

	public string gender;
	public int percentage;
	public string creationDate;

	public string lastUpdate;

	public Int64 projectId;

	public override string ToString ()
	{
		return string.Format ("[Public: Id={0}, ageRange={1},  gender={2}, percentage={3}, creationDate={4}, lastUpdate={5}, projectId={6}]", 
								id, ageRange, gender, percentage, creationDate, lastUpdate, projectId);
	}

	
}
