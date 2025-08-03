using System;
using _Game._Scripts.Logic;
using UnityEngine;

namespace _Game._Scripts.UI
{
    public class ActorUI: MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        private IHealth _health;

        public void Initialize(IHealth heroHealth)
        {
            _health = heroHealth;
            _health.HealthChanged += UpdateHealthBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
            
            if (health != null) 
                Initialize(health);
        }

        private void UpdateHealthBar()
        {
            _progressBar.SetValue(_health.Current, _health.Max);
        }
    }
}