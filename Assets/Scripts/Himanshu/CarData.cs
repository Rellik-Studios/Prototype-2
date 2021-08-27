using UnityEngine;

namespace Himanshu
{
    [CreateAssetMenu( menuName = "ScriptableObjects/CarData", order = 2)]
    public class CarData : ScriptableObject
    {
        public float speed;
        public float accelaration;
        public float handling;
        public float boostDur;
        public float health;

        public GameObject car;
    }
}