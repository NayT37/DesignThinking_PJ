using SQLite4Unity3d;
using System;

public class EmpathyMap  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public int projectId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[EmpathyMap: Id={0},  percentage={1}, creationDate={2}, projectIdId={3}, lastUpdate={4}]", 
								id, percentage, creationDate, projectId, lastUpdate);
	}

	
}
