using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_WandDB", menuName = "Jowarts/WandDB", order = 3)]
public class WandDB : ScriptableObject
{
    public List<WandSO> wands;
    public enum WandName
    {
        [Description("Speedifren")] Speed = 0,
        [Description("Varita Precoz")] FireCooldown  = 1,
        [Description("Palo Cobarde")] ShieldCooldown = 2,
        [Description("Varita Rusa")] InstaKill  = 3,
        [Description("La Venenos")] Poison = 4,
        [Description("Manguito Clasicus")] Heal = 5,
        [Description("Reflectius Maxima")] Reflect = 6
    }
    
    public WandSO GetWandByName(WandName wandName) => wands.FirstOrDefault(wand => wand.wandName == wandName);
}