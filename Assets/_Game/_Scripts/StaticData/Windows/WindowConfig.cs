using System;
using _Game._Scripts.UI.Services.Windows;
using _Game._Scripts.UI.Windows;

namespace _Game._Scripts.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}