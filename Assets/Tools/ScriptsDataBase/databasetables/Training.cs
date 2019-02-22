using SQLite4Unity3d;
using System;

public class Training
{

    [PrimaryKey]
    public Int64 id { get; set; }
    public string name { get; set; }
    public int percentage { get; set; }
    public string creationDate { get; set; }
    public Int64 groupId { get; set; }
    public string lastUpdate { get; set; }

    /* public int id;
    public string name;
    public int percentage;
    public string creationDate;

    public int groupId;
    public string lastUpdate; */

    public override string ToString()
    {
        return string.Format("[Training: Id={0}, Name={1},  percentage={2}, creationDate={3}, groupId={4}, lastUpdate={5}]",
                                id, name, percentage, creationDate, groupId, lastUpdate);
    }


}
