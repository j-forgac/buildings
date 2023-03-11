using System.Diagnostics;
using Buildings.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buildings.Controllers;

public class RoomController : Controller
{
    private readonly ILogger<RoomRepository> _logger;

    private BuildingsService _buildingsService = new BuildingsService();
    private RoomRepository _roomRepository = new RoomRepository();
    private MeetingRepository _meetingRepository = new MeetingRepository();

    public RoomController(ILogger<RoomRepository> logger)
    {
        _logger = logger;
    }

    public IActionResult Room(int roomId)
    {
        FillViewData(roomId);
        ViewData["InvalidMeeting"] = false;
        return View();
    }

    [HttpPost]
    public IActionResult UpdateRoom(int roomId, string roomName, string roomDescription)
    {
        _roomRepository.Update(roomId, roomName, roomDescription);
        FillViewData(roomId);
        ViewData["InvalidMeeting"] = false;
        return View("Room");
    }

    [HttpPost]
    public IActionResult AddMeeting(int roomId, string meetingName, string meetingDescription, DateTime from, DateTime to)
    {
        if (from > to || _meetingRepository.GetAllByRoom(roomId).Any(x => x.Value.Collides(from, to)))
        {
            ViewData["InvalidMeeting"] = true;
        }
        else
        {
            ViewData["InvalidMeeting"] = false;
            _meetingRepository.Insert(roomId, meetingName, meetingDescription, from, to);
        }

        FillViewData(roomId);
        return View("Room");
    }

    [HttpPost]
    public IActionResult DeleteMeeting(int meetingId)
    {
        int roomId = _meetingRepository.GetMeeting(meetingId).RoomId;
        _buildingsService.DeleteMeeting(meetingId);
        ViewData["InvalidMeeting"] = false;
        FillViewData(roomId);
        return View("Room");
    }

    private void FillViewData(int roomId)
    {
        ViewData["room_id"] = roomId;
        Room room = _roomRepository.GetRoom(roomId);
        ViewData["room_name"] = room.Name;
        ViewData["room_description"] = room.Description;
        ViewData["meetings"] = _meetingRepository.GetAllByRoom(roomId);
    }
}