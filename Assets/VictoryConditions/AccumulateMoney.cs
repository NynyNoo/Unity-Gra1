using UnityEngine;
using System.Collections;
using RTS;

public class AccumulateMoney : VictoryCondition
{

    public int amount = 7000;

    private ResourceType type = ResourceType.Gold;

    public override string GetDescription()
    {
        return "Accumulating Money";
    }

    public override bool PlayerMeetsConditions(Player player)
    {
        return player && !player.IsDead() && player.GetResourceAmount(type) >= amount;
    }
}