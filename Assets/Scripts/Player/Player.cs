using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;
using Newtonsoft.Json;
public class Player : MonoBehaviour
{
    public string username;
    public bool human;
    public HUD hud;
    public Color teamColor;
    public WorldObject SelectedObject { get; set; }
    public int startGold, startFood;
    private Dictionary<ResourceType, int> resources, resourceLimits;
    

    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponentInChildren<HUD>();
        AddStartResources();
    }

    // Update is called once per frame
    void Update()
    {
        if (human)
        {
            hud.SetResourceValues(resources);
        }
    }
    void Awake()
    {
        resources = InitResourceList();
    }
    private Dictionary<ResourceType, int> InitResourceList()
    {
        Dictionary<ResourceType, int> list = new Dictionary<ResourceType, int>();
        list.Add(ResourceType.Gold, 0);
        list.Add(ResourceType.Food, 0);
        return list;

    }

    private void AddStartResources()
    {
        AddResource(ResourceType.Gold, startGold);
        AddResource(ResourceType.Food, startFood);
    }
    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;
    }
    public void GimmeUnit(string unitName, Vector3 spawnPoint , Vector3 rallyPoint)
    {
        //barak = player.GetComponent<Baraki>();
        Units units = GetComponentInChildren<Units>();
        GameObject newUnit = (GameObject)Instantiate(ResourceManager.GetUnit(unitName), spawnPoint, Quaternion.Euler(0, 30, 0));
        newUnit.transform.parent = units.transform;
        Unit unitObject = newUnit.GetComponent<Unit>();
        if (unitObject)
        {
            unitObject.ObjectId = ResourceManager.GetNewObjectId();
            if (spawnPoint != rallyPoint) unitObject.StartMove(rallyPoint);

        }
    }
    public void AddUnit(string unitName, Vector3 spawnPoint, Vector3 rallyPoint, Quaternion rotation, Building creator)
    {
        Units units = GetComponentInChildren<Units>();
        GameObject newUnit = (GameObject)Instantiate(ResourceManager.GetUnit(unitName), spawnPoint, rotation);
        newUnit.transform.parent = units.transform;
        Unit unitObject = newUnit.GetComponent<Unit>();
        if (unitObject)
        {
            unitObject.ObjectId = ResourceManager.GetNewObjectId();
            if (spawnPoint != rallyPoint) unitObject.StartMove(rallyPoint);
        }
        else Destroy(newUnit);
    }
    public virtual void SaveDetails(JsonWriter writer)
    {
        SaveManager.WriteString(writer, "Username", username);
        SaveManager.WriteBoolean(writer, "Human", human);
        SaveManager.WriteColor(writer, "TeamColor", teamColor);
        SaveManager.SavePlayerBuildings(writer, GetComponentsInChildren<Building>());
        SaveManager.SavePlayerUnits(writer, GetComponentsInChildren<Unit>());
    }
    public bool IsDead()
    {
        Building[] buildings = GetComponentsInChildren<Building>();
        if (buildings != null && buildings.Length > 0) return false;
        return true;
    }
    public int GetResourceAmount(ResourceType type)
    {
        return resources[type];
    }
    public void RemoveResource(ResourceType type, int amount)
    {
        resources[type] -= amount;
    }
}
