using SQLite4Unity3d;
using System;

public class Field  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int percentage { get; set; }

	public string description { get; set; }
	public string creationDate { get; set; }

	public string lastUpdate { get; set; }

	public int problemId { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Field: Id={0}, Name={1},  description={2}, percentage={3}, creationDate={4}, lastUpdate={5}, problemId={6}]", 
								id, name, description, percentage, creationDate, lastUpdate, problemId);
	}

	
}
