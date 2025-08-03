using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public static class PhysicsDebug
    {
        public static void DrawDebugSphere(Vector3 position, float radius, float seconds, Color color)
        {
            Debug.DrawRay(position, radius * Vector3.up, color, seconds);
            Debug.DrawRay(position, radius * Vector3.down, color, seconds);
            Debug.DrawRay(position, radius * Vector3.left, color, seconds);
            Debug.DrawRay(position, radius * Vector3.right, color, seconds);
            Debug.DrawRay(position, radius * Vector3.forward, color, seconds);
            Debug.DrawRay(position, radius * Vector3.back, color, seconds);
        }
    }
}