namespace Buildings.Models;

public class BuildingsService
{
    private BuildingRepository _buildingRepository = new BuildingRepository();
    private RoomRepository _roomRepository = new RoomRepository();
    private MeetingRepository _meetingRepository = new MeetingRepository();

    public BuildingsService()
    {
    }

    public void DeleteBuilding(int buildingId)
    {
        foreach (var room in _roomRepository.GetAllByBuilding(buildingId))
        {
            DeleteRoom(room.Value.Id);
        }
        _buildingRepository.Delete(buildingId);
    }
    
    public void DeleteRoom(int roomId)
    {
        _meetingRepository.DeleteAllByRoom(roomId);
        _roomRepository.Delete(roomId);
    }
    
    public void DeleteMeeting(int meetingId)
    {
        _meetingRepository.Delete(meetingId);
    }
    
}