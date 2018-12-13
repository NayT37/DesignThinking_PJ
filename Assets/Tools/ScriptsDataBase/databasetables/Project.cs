using SQLite4Unity3d;
using System;

public class Project  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public string sectorName { get; set; }

	public int groupId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Project: Id={0}, Name={1},  percentage={2}, creationDate={3}, sectorName={4}, groupId={5}, lastUpdate={6}]", 
								id, name, percentage, creationDate, sectorName, groupId, lastUpdate);
	}

	
}
