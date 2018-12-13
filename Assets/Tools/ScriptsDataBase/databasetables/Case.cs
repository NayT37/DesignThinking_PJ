using SQLite4Unity3d;
using System;

public class Case  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public int trainingId { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Case: Id={0}, Name={1},  percentage={2}, creationDate={3}, trainingId={4}, lastUpdate={5}]", 
								id, name, percentage, creationDate, trainingId, lastUpdate);
	}

	
}
