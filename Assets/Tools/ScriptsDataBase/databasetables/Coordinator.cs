using SQLite4Unity3d;
using System;

public class Coordinator  {

	[PrimaryKey, AutoIncrement]
	public string identityCard { get; set; }
	public int documentTypeId { get; set; }

	public string name { get; set; }

	public string surname { get; set; }

	public string phone { get; set; }

	public string address { get; set; }
	public string password { get; set; }
	public string creationDate { get; set; }
	public int zoneId { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Coordinator: IdentityCard={0}, documentTypeId={1},  name={2}, surname={3}, phone={4}, address={5}, password={6}, creationDate={7}, zoneId={8}]", 
								identityCard, documentTypeId, name, surname, phone, address, password, creationDate, zoneId);
	}

	
}
