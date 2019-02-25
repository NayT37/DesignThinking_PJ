using SQLite4Unity3d;
using System;

[Serializable]
public class TrainingWeb
{

    // [PrimaryKey, AutoIncrement]
    // public int id { get; set; }
    // public string name { get; set; }
    // public int percentage { get; set; }
    // public string creationDate { get; set; }
    // public int groupId { get; set; }
    // public string lastUpdate { get; set; }

    public Int64 id;
    public string name;
    public int percentage;
    public string creationDate;

    public Int64 groupId;
    public string lastUpdate; 

    public override string ToString()
    {
        return string.Format("[Training: Id={0}, Name={1},  percentage={2}, creationDate={3}, groupId={4}, lastUpdate={5}]",
                                id, name, percentage, creationDate, groupId, lastUpdate);
    }


}
