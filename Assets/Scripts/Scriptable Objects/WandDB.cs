using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_WandDB", menuName = "Jowarts/WandDB", order = 3)]
public class WandDB : ScriptableObject
{
    public List<WandSO> wands;
    public enum WandName
    {
        Speed = 0,
        FireCooldown  = 1,
        ShieldCooldown = 2,
        InstaKill  = 3,
        Poison = 4,
        Heal = 5,
        Reflect = 6
    }
    
    public WandSO GetWandByName(WandName wandName) => wands.FirstOrDefault(wand => wand.wandName == wandName);
}