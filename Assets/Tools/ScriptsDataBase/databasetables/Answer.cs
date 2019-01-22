using SQLite4Unity3d;
using System;

// [Serializable]
public class Answer
{

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public int counter { get; set; }
    public int value { get; set; }
    public string creationDate { get; set; }
    public string lastUpdate { get; set; }

    public int questionId { get; set; }
    /* 	 
        public int id ;

        public int counter ;
        public int value ;
        public string creationDate ;
        public string lastUpdate ;

        public int questionId ;

 */
    public override string ToString()
    {
        return string.Format("[Answer: Id={0}, counter={1}, value={2}, creationDate={3}, description{4}, questionId={5}]",
                                id, counter, value, creationDate, lastUpdate, questionId);
    }
}
