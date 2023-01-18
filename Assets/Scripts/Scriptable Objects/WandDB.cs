using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_WandDB", menuName = "Jowarts/WandDB", order = 3)]
public class WandDB : ScriptableObject
{
    public List<WandSO> wands;
    public enum WandName
    {
        Gryffindor = 0,
        Slytherin  = 1,
        Hufflepuff = 2,
        Ravenclaw  = 3
    }
    
    public WandSO GetWandByName(WandName wandName) => wands.FirstOrDefault(wand => wand.wandName == wandName);
}