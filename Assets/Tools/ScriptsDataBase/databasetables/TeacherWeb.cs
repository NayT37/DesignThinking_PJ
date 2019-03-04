using SQLite4Unity3d;
using System;

[Serializable]
public class TeacherWeb  {

	public string identityCard;
	public int documentTypeId;

	public string names;

	public string surnames;

	public string phone;

	public string address;

	public string email;
	public string password;
	public string creationDate;
	public string headquartersId;

	public override string ToString ()
	{
		return string.Format ("[Teacher: IdentityCard={0}, documentTypeId={1},  name={2}, surname={3}, phone={4}, address={5}, email={6} password={7}, creationDate={8}, headquartersId={9}]", 
								identityCard, documentTypeId, names, surnames, phone, address, email, password, creationDate, headquartersId);
	}

	
}
