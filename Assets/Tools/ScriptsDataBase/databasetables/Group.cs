using SQLite4Unity3d;
using System;

[Serializable]
public class Group
{

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public string name { get; set; }
    public int percentage { get; set; }
    public string creationDate { get; set; }
    public int studentsCounter { get; set; }

    public int courseId { get; set; }
    public string lastUpdate { get; set; }

    public override string ToString()
    {
        return string.Format("[Group: Id={0}, Name={1},  percentage={2}, creationDate={3}, studentsCounter={4}, courseId={5}, lastUpdate={6}]",
                            id, name, percentage, creationDate, studentsCounter, courseId, lastUpdate);
    }

    /* 	public int id;
        public string name;
        public int percentage;
        public string creationDate;

        public int studentsCounter;

        public int courseId;
        public string lastUpdate;

*/


}
