using SQLite4Unity3d;
using System;

public class DocumentType  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string name { get; set; }

	public override string ToString ()
	{
		return string.Format ("[DocumentType: Id={0}, Name={1}]", id, name);
	}

	
}
