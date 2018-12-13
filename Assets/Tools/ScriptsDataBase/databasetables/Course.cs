using SQLite4Unity3d;
using System;

public class Course  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int percentage { get; set; }
	public string creationDate { get; set; }

	public string teacherIdentityCard { get; set; }
	public string lastUpdate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Course: Id={0}, Name={1},  percentage={2}, creationDate={3}, teacherIdentityCard={4}, lastUpdate={5}]", 
								id, name, percentage, creationDate, teacherIdentityCard, lastUpdate);
	}

	
}
