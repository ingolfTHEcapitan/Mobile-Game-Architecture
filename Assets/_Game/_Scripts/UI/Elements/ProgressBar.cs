using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.UI.Elements
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        public void SetValue(float current, float max)
        {
            _image.fillAmount = current / max;
            _text.text = $"{current}/{max}";
        }
    }
}