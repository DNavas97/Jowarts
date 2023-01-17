using UnityEngine;

[CreateAssetMenu(fileName = "SO_House", menuName = "Jowarts/House", order = 1)]
public class HouseSO : ScriptableObject
{
    public HouseDB.HouseName houseName;
    public Sprite houseIcon;
}