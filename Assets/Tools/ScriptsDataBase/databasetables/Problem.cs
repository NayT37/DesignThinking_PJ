using SQLite4Unity3d;
using System;

public class Problem  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public string lastUpdate { get; set; }

	public int projectId { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Problem: Id={0}, Name={1},  percentage={2}, creationDate={3}, lastUpdate={4}, projectId={5}]", 
								id, name, percentage, creationDate, lastUpdate, projectId);
	}

	
}
