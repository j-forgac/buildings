namespace Buildings.Models;

public class Meeting
{
    private int _id;
    private int _roomId;
    private string _name;
    private string _description;
    private DateTime _from;
    private DateTime _to;

    #region GettersAndSetters
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public int RoomId
    {
        get => _roomId;
        set => _roomId = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public DateTime From
    {
        get => _from;
        set => _from = value;
    }

    public DateTime To
    {
        get => _to;
        set => _to = value;
    }
    #endregion
    
    public Meeting(int id, int roomId,string name, string description, DateTime from, DateTime to)
    {
        _id = id;
        _roomId = roomId;
        _name = name;
        _description = description;
        _from = from;
        _to = to;
    }
    
    public Meeting(string data)
    {
        string[] fields = data.Split(", ");
        _id = int.Parse(fields[0]);
        _roomId = int.Parse(fields[1]);
        _name = fields[2];
        _description = fields[3];
        _from = DateTime.Parse(fields[4]);
        _to = DateTime.Parse(fields[5]);
    }

    public string ToString()
    {
        return string.Format("{0}, {1}, {2}, {3}, {4}, {5}", _id, _roomId, _name, _description,_from,_to);
    }

    public bool Collides(DateTime fromOther, DateTime toOther)
    {
        return InBetween(fromOther, _from, _to) || InBetween(toOther, _from, _to) || InBetween(_from,fromOther,toOther) || InBetween(_to,fromOther,toOther);
    }

    private bool InBetween(DateTime date, DateTime from, DateTime to)
    {
        return date >= from && date <= to;
    }
    
    
    public string NormalizeDateTime(DateTime d)
    {
        return string.Format("{0}-{1}-{2}T{3}:{4}:{5}", Pad(d.Year,4), Pad(d.Month,2), Pad(d.Day,2), Pad(d.Hour,2),Pad(d.Minute,2),Pad(d.Second,2));
    }

    private string Pad(int i, int length)
    {
        string str = i.ToString();
        return str.PadLeft(length,'0');
    }
}