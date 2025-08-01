using _Game._Scripts.Logic;
using UnityEngine;

namespace _Game._Scripts.Hero
{
    public class ActorUI: MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        private HeroHealth _heroHealth;

        private void OnDestroy()
        {
            _heroHealth.HealthChanged -= UpdateHealthBar;
        }

        public void Initialize(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            _progressBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}