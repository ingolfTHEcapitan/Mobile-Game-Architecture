using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI.Elements
{
    public class RawImageScroller : MonoBehaviour
    {
        public float horizontalSpeed;
        public float verticalSpeed;

        private RawImage _rawImage;

        public void Start() => 
            _rawImage = GetComponent<RawImage>();

        public void Update()
        {
            Rect currentUV = _rawImage.uvRect;
            currentUV.x -= Time.deltaTime * horizontalSpeed;
            currentUV.y -= Time.deltaTime * verticalSpeed;

            if (currentUV.x <= -1f || currentUV.x >= 1f) 
                currentUV.x = 0f;

            if (currentUV.y <= -1f || currentUV.y >= 1f) 
                currentUV.y = 0f;

            _rawImage.uvRect = currentUV;
        }
    }
}