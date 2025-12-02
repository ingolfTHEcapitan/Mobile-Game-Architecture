using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Editor
{
    public class Tools
    {
        [MenuItem("Tools/ClearPlayerPrefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs очищены!");
        }
    }
}