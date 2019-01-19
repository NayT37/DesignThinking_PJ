using SQLite4Unity3d;
using System;

[Serializable]
public class Course  {
	// Solo funciona en móvil
	// [PrimaryKey, AutoIncrement]
	// public int id { get; set; }
	// public string name { get; set; }
	// public object percentage { get; set; }
	// public string creationDate { get; set; }

	// public string teacherIdentityCard { get; set; }
	// public string lastUpdate { get; set; }
	public int id;
	public string name;
	public int percentage;
	public string creationDate;

	public string teacherIdentityCard;
	public string lastUpdate;

	public override string ToString ()
	{
		return string.Format ("[Course: Id={0}, Name={1},  percentage={2}, creationDate={3}, teacherIdentityCard={4}, lastUpdate={5}]", 
								id, name, percentage, creationDate, teacherIdentityCard, lastUpdate);
	}

	
}
