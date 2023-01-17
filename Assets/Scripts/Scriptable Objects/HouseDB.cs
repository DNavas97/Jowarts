using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_HouseDB", menuName = "Jowarts/HouseDB", order = 3)]
public class HouseDB : ScriptableObject
{
    public List<HouseSO> houses;
    public enum HouseName
    {
        Gryffindor = 0,
        Slytherin  = 1,
        Hufflepuff = 2,
        Ravenclaw  = 3
    }
    
    public HouseSO GetHouseByName(HouseName houseName) => houses.FirstOrDefault(house => house.houseName == houseName);
}