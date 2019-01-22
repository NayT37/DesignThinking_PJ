using SQLite4Unity3d;
using System;

// [Serializable]
public class Teacher  {

	[PrimaryKey]
	public string identityCard { get; set; }
	public int documentTypeId { get; set; }

	public string names { get; set; }

	public string surnames { get; set; }

	public string phone { get; set; }

	public string address { get; set; }

	public string email { get; set; }
	public string password { get; set; }
	public string creationDate { get; set; }
	public int headquartersId { get; set; }
	// public string identityCard;
	// public int documentTypeId;

	// public string names;

	// public string surnames;

	// public string phone;

	// public string address;

	// public string email;
	// public string password;
	// public string creationDate;
	// public int headquartersId;

	public override string ToString ()
	{
		return string.Format ("[Teacher: IdentityCard={0}, documentTypeId={1},  name={2}, surname={3}, phone={4}, address={5}, email={6} password={7}, creationDate={8}, headquartersId={9}]", 
								identityCard, documentTypeId, names, surnames, phone, address, email, password, creationDate, headquartersId);
	}

	
}
