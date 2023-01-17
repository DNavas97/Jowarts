using UnityEngine;

[CreateAssetMenu(fileName = "SO_Wand", menuName = "Jowarts/Wand", order = 1)]
public class WandSO : ScriptableObject
{
    public WandDB.WandName wandName;
    public Sprite wandIcon;
}