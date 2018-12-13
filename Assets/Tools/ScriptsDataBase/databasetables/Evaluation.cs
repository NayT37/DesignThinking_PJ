using SQLite4Unity3d;
using System;

public class Evaluation  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public int mindMapId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Evaluation: Id={0},  percentage={1}, creationDate={2}, mindMapId={3}, lastUpdate={4}]", 
								id, percentage, creationDate, mindMapId, lastUpdate);
	}

	
}
