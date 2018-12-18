using SQLite4Unity3d;
using System;

public class Problem  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public string lastUpdate { get; set; }

	public int projectId { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Problem: Id={0}, percentage={1}, creationDate={2}, lastUpdate={3}, projectId={4}]", 
								id, percentage, creationDate, lastUpdate, projectId);
	}

	
}
