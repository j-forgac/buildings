namespace Buildings.Models;

public class RoomRepository
{
    public static Dictionary<int,Room> Rooms = new Dictionary<int,Room>();
    private string _filePath = "Data/rooms.csv";
    private string _tableColumns = "id, building_id, name, description";

    public RoomRepository()
    {
        ConvertCsvToMap();
    }

    public Room GetRoom(int id)
    {
        ConvertCsvToMap();
        return Rooms[id];
    }
    
    public Dictionary<int,Room> GetAllByBuilding(int buildingId)
    {
        return Rooms.Where(pair => pair.Value.BuildingId == buildingId).ToDictionary(pair => pair.Key,pair => pair.Value);
    }
    
    public void Insert(int buildingId, string name, string description)
    {
        ConvertCsvToMap();
        int id = Rooms.Count == 0 ? 0 : Rooms.Last().Value.Id + 1;
        Rooms[id] = new Room(id,buildingId,name,description);
        ConvertMapToCsv();
    }
    
    public void Update(int id, string name, string description)
    {
        ConvertCsvToMap();
        Rooms[id].Name = name;
        Rooms[id].Description = description;
        ConvertMapToCsv();
    }

    public void Delete(int id)
    {
        ConvertCsvToMap();
        Rooms.Remove(id);
        ConvertMapToCsv();
    }
    
    private void ConvertMapToCsv()
    {
        File.WriteAllText(_filePath, _tableColumns);
        foreach (var room in Rooms)
        {
            File.AppendAllText(_filePath, "\n"+room.Value.ToString());
        }
    }

    private void ConvertCsvToMap()
    {
        Rooms.Clear();
        foreach (var roomData in File.ReadAllLines(_filePath).Skip(1))
        {
            Room room = new Room(roomData);
            Rooms[room.Id] = room;
        }
    }
}