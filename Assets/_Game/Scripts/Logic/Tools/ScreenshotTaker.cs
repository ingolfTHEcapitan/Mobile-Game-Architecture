using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.Logic.Tools
{
    public class ScreenshotTaker : MonoBehaviour
    {
#if UNITY_EDITOR
        
        private const string Name = "screenshot";
        private const string FileExtension = ".png";
        
        [SerializeField] private string _folderPath;
        [SerializeField] [Range(1, 2)] private int _imageSize = 1;
        [SerializeField] private KeyCode _keyCode = KeyCode.K;

        private void Start()
        {
            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(_keyCode))
                StartCoroutine(TakeScreenshot());
        }

        private IEnumerator TakeScreenshot()
        {
            yield return new WaitForEndOfFrame();
            ScreenCapture.CaptureScreenshot($"{_folderPath}//{CreateFileName()}", _imageSize);
            Debug.Log("Screenshot taken");
        }

        private string CreateFileName() => 
            $"{Name}-{DateTime.Now:yyyy-MM-dd-HH-mm-ssffff}{FileExtension}";
#endif
    }
}