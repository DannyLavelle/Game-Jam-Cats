using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [CreateAssetMenu(fileName = "NewUpgrade", menuName = "ScriptableObject/Upgrade")]
public class Upgrades : ScriptableObject
{
    public string nameofupgrade;
    public int level;
    public int baseCost;
    public int costIncreaseAmount;
    public int cost;

    public enum upgradeType { addative,multiplicative }
    public upgradeType Type;
    [Tooltip("the increase each level gives, addative or multiplicative")]
    public float increaseByLevelUp;
    public float GetMultiplier()
    {
        float multiplier;
        if(Type == upgradeType.multiplicative)
        {
            multiplier = Mathf.Pow(increaseByLevelUp, level);
        }
        else
        {
            multiplier = increaseByLevelUp*level;
        }
        return multiplier;
    }
    public void increaseLevel()
    {
        level++;
        cost = Mathf.CeilToInt(cost * costIncreaseAmount);
    }
}

