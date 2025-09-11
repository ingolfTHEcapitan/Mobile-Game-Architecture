using UnityEngine;

namespace _Game._Scripts.Logic.Triggers
{
    public abstract class TriggerBase : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private Color32 _boxColor = new(15, 161, 49, 130);

        public BoxCollider BoxCollider
        {
            get => _boxCollider;
            private set => _boxCollider = value; 
        }
        
        public Color32 BoxColor => _boxColor;
    }
}