using System;
using UnityEngine;

namespace CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;
        public event Action OnDecreaseValue;
        public event Action OnIncreaseValue;
        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue
        {
            get => currentValue;
            set
            {
                float oldValue = currentValue;
                currentValue = Mathf.Clamp(value, 0f, MaxValue);

                if (currentValue <= 0f)
                {
                    OnCurrentValueZero?.Invoke();
                }
                else if (currentValue < oldValue)
                {
                    OnDecreaseValue?.Invoke();
                }
                if (currentValue <= 0f)
                {
                    OnIncreaseValue?.Invoke();
                }
            }
        }

        private float currentValue;

        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;

        public void Update(float amount)
        {
            MaxValue += amount;
            currentValue = MaxValue;
        }
    }
}