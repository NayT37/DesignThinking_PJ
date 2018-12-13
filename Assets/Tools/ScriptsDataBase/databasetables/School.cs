using SQLite4Unity3d;
using System;

public class School  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int townId { get; set; }
	public string creationDate { get; set; }
	public int percentage { get; set; }

	public override string ToString ()
	{
		return string.Format ("[School: Id={0}, Name={1},  townId={2}, creationDate={3}, percentage={4}]", 
								id, name, townId, creationDate, percentage);
	}

	
}
