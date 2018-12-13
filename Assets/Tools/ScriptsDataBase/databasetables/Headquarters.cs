using SQLite4Unity3d;
using System;

public class Headquarters  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int schoolId { get; set; }
	public string creationDate { get; set; }
	public int percentage { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Headquarter: Id={0}, Name={1},  schoolId={2}, creationDate={3}, percentage={4}]", 
								id, name, schoolId, creationDate, percentage);
	}

	
}
