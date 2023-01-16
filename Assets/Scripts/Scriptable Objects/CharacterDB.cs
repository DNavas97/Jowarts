using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_CharacterDB", menuName = "Jowarts/CharacterDB", order = 2)]
public class CharacterDB : ScriptableObject
{
    public List<CharacterSO> characters;
    
    public enum CharacterName
    {
        Voldemort = 0,
        Hagrid    = 1,
        Harry     = 2,
        Ron       = 3,
        Hermione  = 4,
        Draco     = 5,
        Snape     = 6,
        Jovani    = 7
    }

    public CharacterSO GetCharacterByName(CharacterName characterName) => characters.FirstOrDefault(character => character.characterName == characterName);
}