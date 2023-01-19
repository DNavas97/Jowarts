using UnityEngine;

namespace Persistent_Data
{
    public static class GlobalParams
    {
        #region Inputs

        public static KeyCode JumpInputP1   = KeyCode.Joystick1Button0;
        public static KeyCode JumpInputP2   = KeyCode.Joystick2Button0;
    
        public static KeyCode SubmitInputP1 = JumpInputP1;
        public static KeyCode SubmitInputP2 = JumpInputP2;
    
        public static KeyCode ShieldInputP1 = KeyCode.Joystick1Button1;
        public static KeyCode ShieldInputP2 = KeyCode.Joystick2Button1;
    
        public static KeyCode FireInputP1 = KeyCode.Joystick1Button2;
        public static KeyCode FireInputP2 = KeyCode.Joystick2Button2;

        public static KeyCode BackButtonP1 = ShieldInputP1;
        public static KeyCode BackButtonP2 = ShieldInputP2;

        #endregion

        #region BaseParams
        
        public static float BaseFireCooldown       = 1f;
        public static float BaseShieldCooldown     = 3f;
        public static float BasePlayerSpeed        = 3.5f;
        public static float BasePlayerJump         = 5.0f;
        public static int   BaseProjectileDamage   = 10;
        public static float BaseProjectileSpeed    = 15.0f;
        public static int   BaseInstaKillProb      = 0;
        public static int   BaseResurrections      = 0;
        public static float BaseProjectileSize     = 1f;
        public static int   BaseHealth             = 100;
        public static float BaseProjectileHeal     = 0f;
        public static float BaseShieldHeal         = 0f;
        public static bool  BaseCanReflect         = false;
        public static int   BasePoisonDamage       = 0;
        public static float PoisonDuration         = 5f;
        
        #endregion

        #region Wizard Synergies

        public static int   HarryResurrections               = 1;
        public static float RonSpeedMultiplier               = 0.5f;
        public static float HermioneProjectileSizeMultiplier = 0.25f;
        public static float DracoJumpMultiplier              = 0.5f;
        public static float VoldemortDamageMultiplier        = 0.25f;
        public static float HagridHealthMultiplier           = 0.2f;
        public static float SnapeShieldCooldownMultiplier    = 0.3f;
        public static float JovaniShieldHeal                 = 0.3f;

        #endregion

        #region Wand Synergies

        public static float WandProjectileSpeedMultiplier    = 0.5f;
        public static float WandProjectileCooldownMultiplier = 0.5f;
        public static float WandShieldCooldownMultiplier     = 0.2f;
        public static int   WandInstaKillProbMultiplier      = 4;
        public static float WandProjectileHealMultiplier     = 0.3f;
        public static int   WandPoisonDamage                 = 5;
        public static bool  WandCanReflect                   = true;

        #endregion
    }
}