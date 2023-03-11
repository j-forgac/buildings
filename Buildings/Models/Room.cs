namespace Buildings.Models;

public class Room
{
    private int _id;
    private int _buildingId;
    private string _name;
    private string _description;

    #region GettersAndSetters
    public int Id => _id;

    public int BuildingId => _buildingId;

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
    #endregion

    public Room(int id, int buildingId,string name, string description)
    {
        _id = id;
        _buildingId = buildingId;
        _name = name;
        _description = description;
    }
    
    public Room(string data)
    {
        string[] fields = data.Split(", ");
        _id = int.Parse(fields[0]);
        _buildingId = int.Parse(fields[1]);
        _name = fields[2];
        _description = fields[3];
    }

    public string ToString()
    {
        return string.Format("{0}, {1}, {2}, {3}", _id, _buildingId, _name, _description);
    }
}
