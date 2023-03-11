using System.Collections;

namespace Buildings.Models;

public class Building
{
    private int _id;
    private string _name;
    private string _description;

    #region GettersAndSetters
    public int Id => _id;

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

    public Building(int id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
    
    public Building(string data)
    {
        string[] fields = data.Split(", ");
        _id = int.Parse(fields[0]);
        _name = fields[1];
        _description = fields[2];
    }

    public string ToString()
    {
        return string.Format("{0}, {1}, {2}", _id, _name, _description);
    }
}