using System;
using _Game.Scripts.UI.Services.Windows;
using _Game.Scripts.UI.Windows;

namespace _Game.Scripts.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}