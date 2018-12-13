using SQLite4Unity3d;
using System;

public class Zone  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }
	public string creationDate { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Zone: Id={0}, Name={1}, creationDate={2}]", id, name,creationDate);
	}

	
}
