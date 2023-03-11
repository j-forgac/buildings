using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Buildings.Models;

namespace Buildings.Controllers;

public class BuildingController : Controller
{
    private readonly ILogger<BuildingController> _logger;

    private BuildingsService _buildingsService = new BuildingsService();
    private BuildingRepository _buildingRepository = new BuildingRepository();
    private RoomRepository _roomRepository = new RoomRepository();
    

    public BuildingController(ILogger<BuildingController> logger)
    {
        _logger = logger;
    }

    public IActionResult Building(int roomId)
    {
        FillViewData(roomId);
        return View();
    }


    [HttpPost]
    public IActionResult UpdateBuilding(int roomId,string buildingName, string buildingDescription)
    {
        _buildingRepository.Update(roomId,buildingName, buildingDescription);
        FillViewData(roomId);
        return View("Building");
    }
    
    [HttpPost]
    public IActionResult AddRoom(int roomId,string roomName, string roomDescription)
    {
        _roomRepository.Insert(roomId,roomName, roomDescription);
        FillViewData(roomId);
        return View("Building");
    }
    
    [HttpPost]
    public IActionResult DeleteRoom(int roomId)
    {
        int buildingId = _roomRepository.GetRoom(roomId).BuildingId;
        _buildingsService.DeleteRoom(roomId);
        FillViewData(buildingId);
        return View("Building");
    }

    private void FillViewData(int buildingId)
    {
        ViewData["building_id"] = buildingId;
        Building building = _buildingRepository.GetBuilding(buildingId);
        ViewData["building_name"] = building.Name;
        ViewData["building_description"] = building.Description;
        ViewData["rooms"] = _roomRepository.GetAllByBuilding(buildingId);
    }
}