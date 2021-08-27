using UnityEngine;

namespace Himanshu
{
    [CreateAssetMenu( menuName = "ScriptableObjects/PowerUpImage", order = 1)]
    public class PowerUpImage : ScriptableObject
    {
        public Sprite powerUpImage;
        public Powertypes powerType;
    }
}
