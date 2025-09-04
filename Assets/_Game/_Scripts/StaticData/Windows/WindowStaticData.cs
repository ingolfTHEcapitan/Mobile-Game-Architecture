using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts.StaticData.Windows
{
    [CreateAssetMenu(fileName = "Window Static Data", menuName = "StaticData/Window")]
    public class WindowStaticData: ScriptableObject
    {
        public List<WindowConfig> Configs = new List<WindowConfig>();
    }
}