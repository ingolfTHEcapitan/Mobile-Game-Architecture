using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.Logic
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetValue(float current, float max) => 
            _image.fillAmount = current / max;
    }
}