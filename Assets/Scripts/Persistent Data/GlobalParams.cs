using UnityEngine;

namespace Persistent_Data
{
    public static class GlobalParams
    {
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
    }
}