using System.Diagnostics;
using System.Globalization;
using Buildings.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buildings.Controllers;

public class MeetingController : Controller
{
    private readonly ILogger<RoomRepository> _logger;

    private BuildingsService _buildingsService = new BuildingsService();
    private MeetingRepository _meetingRepository = new MeetingRepository();

    public MeetingController(ILogger<RoomRepository> logger)
    {
        _logger = logger;
    }

    public IActionResult Meeting(int meetingId)
    {
        FillViewData(meetingId);
        ViewData["InvalidMeeting"] = false;
        return View();
    }

    [HttpPost]
    public IActionResult UpdateMeeting(int meetingId, string meetingName, string meetingDescription, DateTime from, DateTime to){
        Console.WriteLine("con. meeting. " + meetingId + " " + meetingDescription + " " + meetingName);
        if (from > to || _meetingRepository.GetAllByRoom(_meetingRepository.GetMeeting(meetingId).RoomId).Any(x => x.Value.Collides(from, to) && x.Value.Id != meetingId))
        {
            ViewData["InvalidMeeting"] = true;
        }
        else
        {
            ViewData["InvalidMeeting"] = false;
            _meetingRepository.Update(meetingId, meetingName, meetingDescription,from,to);
        }
        FillViewData(meetingId);
        return View("Meeting");
    }

    private void FillViewData(int meetingId)
    {
        ViewData["meeting_id"] = meetingId;
        Meeting meeting = _meetingRepository.GetMeeting(meetingId);
        ViewData["meeting_name"] = meeting.Name;
        ViewData["meeting_description"] = meeting.Description;
        ViewData["meeting_from"] = meeting.NormalizeDateTime(meeting.From);
        ViewData["meeting_to"] =meeting.NormalizeDateTime(meeting.To);
    }

}