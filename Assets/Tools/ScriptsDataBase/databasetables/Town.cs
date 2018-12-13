using SQLite4Unity3d;
using System;

public class Town  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public int zoneId { get; set; }
	public string creationDate { get; set; }
	public int percentage { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Town: Id={0}, Name={1},  zoneId={2}, creationDate={3}, percentage={4}]", 
								id, name, zoneId, creationDate, percentage);
	}

	
}
