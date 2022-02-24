using UnityEngine;
using RTS;
using Newtonsoft.Json;

public class Resource : WorldObject
{

    protected ResourceType resourceType;
    protected float timer;
    protected float delayAmount;
    protected int income;
    
    protected override void Start()
    {
        base.Start();
        resourceType = ResourceType.Gold;
        delayAmount = 5;
        income = 20;
    }

    /*** Public methods ***/
    protected override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayAmount)
        {
            timer = 0f;
            player.AddResource(resourceType, income);
        }
    }
    public ResourceType GetResourceType()
    {
        return resourceType;
    }
    protected override bool ShouldMakeDecision()
    {
        return false;
    }
}