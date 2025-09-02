using System.Collections;
using UnityEngine;

namespace _Game._Scripts.UI.Elements
{
    public class LoadingCurtain: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private float _fadeStep = 0.03f;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _fadeStep;
                yield return new WaitForSeconds(_fadeStep);
            }
            
            gameObject.SetActive(false);
        }
    }
}