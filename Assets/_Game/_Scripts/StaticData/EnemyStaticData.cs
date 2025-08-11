using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyStaticData", menuName = "StaticData/Enemy")]
public class EnemyStaticData : ScriptableObject
{
    public EnemyesTypeId EnemyTypeId;

    [Range(1f, 200f)]
    public float Helth = 100f;

    [Range(1f, 100f)]
    public float Damage = 10f;

    [Range(0.5f, 10f)]
    public float AttackCooldown = 3.0f;

    [Range(0.5f, 3f)]
    public float AttackRadius = 0.5f;

    [Range(0.5f, 3f)]
    public float AttackDistance = 0.5f;

    [Range(1f, 5f)]
    public float MoveSpeed = 2.5f;

    public GameObject Model;
}