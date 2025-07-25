using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public static class PhysicsDebug
    {
        public static void DrawDebugSphere(Vector3 position, float radius, float seconds)
        {
            Debug.DrawRay(position, radius * Vector3.up, Color.red, seconds);
            Debug.DrawRay(position, radius * Vector3.down, Color.red, seconds);
            Debug.DrawRay(position, radius * Vector3.left, Color.red, seconds);
            Debug.DrawRay(position, radius * Vector3.right, Color.red, seconds);
            Debug.DrawRay(position, radius * Vector3.forward, Color.red, seconds);
            Debug.DrawRay(position, radius * Vector3.back, Color.red, seconds);
        }
    }
}