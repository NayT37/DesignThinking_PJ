using SQLite4Unity3d;
using System;

// [Serializable]
public class Cases
{

    [PrimaryKey]
    //Added { get; set; } to every variable so it can work
    public Int64 id { get; set; }
    public string name { get; set; }
    public int percentage { get; set; }
    public string creationDate { get; set; }

    public Int64 trainingId { get; set; }
    public string lastUpdate { get; set; }

    /* 	public int id;
        public string name;
        public int percentage;
        public string creationDate;

        public int trainingId;
        public string lastUpdate;

 */

    public override string ToString()
    {
        return string.Format("[Case: Id={0}, Name={1},  percentage={2}, creationDate={3}, trainingId={4}, lastUpdate={5}]",
                                id, name, percentage, creationDate, trainingId, lastUpdate);
    }


}
