using UnityEngine;

[CreateAssetMenu(fileName = "SO_Character", menuName = "Jowarts/Character", order = 0)]
public class CharacterSO : ScriptableObject
{
    public CharacterDB.CharacterName characterName;
    public Sprite characterIcon;
}