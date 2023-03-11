namespace Buildings.Models;

public class MeetingRepository
{
    public static Dictionary<int,Meeting> Meetings = new Dictionary<int,Meeting>();
    private string _filePath = "Data/meetings.csv";
    private string _tableColumns = "id, room_id, name, description, from, to";

    public MeetingRepository()
    {
        ConvertCsvToMap();
    }

    public Dictionary<int,Meeting> GetAllByRoom(int roomId)
    {
        return Meetings.Where(pair => pair.Value.RoomId == roomId).ToDictionary(pair => pair.Key,pair => pair.Value);
    }

    public Meeting GetMeeting(int meetingId)
    {
        return Meetings[meetingId];
    }

    public void DeleteAllByRoom(int roomId)
    {
        ConvertCsvToMap();
        foreach (var meetingCopy in new Dictionary<int,Meeting>(Meetings))
        {
            if (meetingCopy.Value.RoomId == roomId)
            {
                Meetings.Remove(meetingCopy.Value.Id);
            }
        }
        ConvertMapToCsv();
    }
    public void Insert(int roomId, string name, string description, DateTime from, DateTime to)
    {
        ConvertCsvToMap();
        int id = Meetings.Count == 0 ? 0 : Meetings.Last().Value.Id + 1;
        Meetings[id] = new Meeting(id,roomId,name,description,from,to);
        ConvertMapToCsv();
    }
    
    public void Update(int id, string name, string description, DateTime from, DateTime to)
    {
        ConvertCsvToMap();
        Meetings[id].Name = name;
        Meetings[id].Description = description;
        Meetings[id].From = from;
        Meetings[id].To = to;
        ConvertMapToCsv();
    }

    public void Delete(int id)
    {
        ConvertCsvToMap();
        Meetings.Remove(id);
        ConvertMapToCsv();
    }

    private void ConvertMapToCsv()
    {
        File.WriteAllText(_filePath, _tableColumns);
        foreach (var room in Meetings)
        {
            File.AppendAllText(_filePath, "\n"+room.Value.ToString());
        }
    }

    private void ConvertCsvToMap()
    {
        Meetings.Clear();
        foreach (var meetingData in File.ReadAllLines(_filePath).Skip(1))
        {
            Meeting meeting = new Meeting(meetingData);
            Meetings[meeting.Id] = meeting;
        }
    }
}