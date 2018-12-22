using SQLite4Unity3d;
using System;

public class Evaluation  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }

	public string category {get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public int mindMapId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Evaluation: Id={0}, category={1},  percentage={2}, creationDate={3}, mindMapId={4}, lastUpdate={5}]", 
								id, category, percentage, creationDate, mindMapId, lastUpdate);
	}

	
}
