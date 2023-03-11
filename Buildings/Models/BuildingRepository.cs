using System.Collections;
using System.Diagnostics;

namespace Buildings.Models;

public class BuildingRepository
{
    public static Dictionary<int,Building> Buildings = new Dictionary<int,Building>();
    private string _filePath = "Data/buildings.csv";
    private string _tableColumns = "id, name, description";

    public BuildingRepository()
    {
        ConvertCsvToMap();
    }

    public Dictionary<int, Building> GetAllBuildings()
    {
        return Buildings;
    }
    public Building GetBuilding(int id)
    {
        ConvertCsvToMap();
        return Buildings[id];
    }
    public void Insert(string name, string description)
    {
        ConvertCsvToMap();
        int id = Buildings.Count == 0 ? 0 : Buildings.Last().Value.Id + 1;
        Buildings[id] = new Building(id,name,description);
        ConvertMapToCsv();
    }
    
    public void Update(int id, string name, string description)
    {
        ConvertCsvToMap();
        Buildings[id].Name = name;
        Buildings[id].Description = description;
        ConvertMapToCsv();
    }

    public void Delete(int id)
    {
        ConvertCsvToMap();
        Buildings.Remove(id);
        ConvertMapToCsv();
    }
    
    private void ConvertMapToCsv()
    {
        File.WriteAllText(_filePath, _tableColumns);
        foreach (var building in Buildings)
        {
            File.AppendAllText(_filePath, "\n"+building.Value.ToString());
        }
    }

    private void ConvertCsvToMap()
    {
        Buildings.Clear();
        foreach (var buildingData in File.ReadAllLines(_filePath).Skip(1))
        {
            Building building = new Building(buildingData);
            Buildings[building.Id] = building;
        }
    }
}