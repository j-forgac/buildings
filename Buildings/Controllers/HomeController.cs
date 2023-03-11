using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Buildings.Models;

namespace Buildings.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private BuildingsService _buildingsService = new BuildingsService();
    private BuildingRepository _buildingRepository = new BuildingRepository();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        FillViewData();
        return View();
    }

    [HttpPost]
    public IActionResult AddBuilding(string name, string description)
    {
        _buildingRepository.Insert(name, description);
        FillViewData();
        return View("Index");
    }
    
    [HttpPost]
    public IActionResult Delete(int roomId)
    {
        _buildingsService.DeleteBuilding(roomId);
        FillViewData();
        return View("Index");
    }


    private void FillViewData()
    {
        ViewData["buildings"] = _buildingRepository.GetAllBuildings();
    }
}