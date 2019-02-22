using SQLite4Unity3d;
using System;

// [Serializable]
public class Project  {

	[PrimaryKey]
	public Int64 id { get; set; }
	public string name { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public string sectorName { get; set; }

	public Int64 groupId { get; set; }
	public string lastUpdate { get; set; }
	// public int id;
	// public string name;
	// public int percentage;
	// public string creationDate;

	// public string sectorName;

	// public int groupId;
	// public string lastUpdate;

	public override string ToString ()
	{
		return string.Format ("[Project: Id={0}, Name={1},  percentage={2}, creationDate={3}, sectorName={4}, groupId={5}, lastUpdate={6}]", 
								id, name, percentage, creationDate, sectorName, groupId, lastUpdate);
	}

	
}
