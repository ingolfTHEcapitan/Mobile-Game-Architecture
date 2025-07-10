using UnityEditor;
using UnityEngine;

namespace _Game._Scripts.Editor
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