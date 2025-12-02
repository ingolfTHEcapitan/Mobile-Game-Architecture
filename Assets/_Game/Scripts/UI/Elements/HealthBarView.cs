using _Game.Scripts.Logic.Common;
using UnityEngine;

namespace _Game.Scripts.UI.Elements
{
    public class HealthBarView: MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        private IHealth _health;

        public void Initialize(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHealthBar;
        }

        public void UpdateHealthBar()
        {
            _progressBar.SetValue(_health.Current, _health.Max);
        }
    }
}