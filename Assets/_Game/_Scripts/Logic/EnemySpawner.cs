using System;
using _Game._Scripts.Data;
using _Game._Scripts.Enemy;
using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;

[RequireComponent(typeof(UniqueId))]
public class EnemySpawner : MonoBehaviour, ISavedProgress
{
    public EnemyesTypeId EnemyeTypeId;

    private bool _slain;
    private string _id;
    private IGameFactory _factory;
    private EnemyDeath _enemyDeath;

    private void Awake()
    {
        _id = GetComponent<UniqueId>().Id;
        _factory = AllServices.Container.Single<IGameFactory>();
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (progress.KillData.SlainSpawners.Contains(_id))
            _slain = true;
        else
            Spawn();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        if (_slain)
            progress.KillData.SlainSpawners.Add(_id);
    }

    private void Spawn()
    {
        GameObject enemy = _factory.CreateEnemy(EnemyeTypeId, transform);
        _enemyDeath = enemy.GetComponent<EnemyDeath>();
        _enemyDeath.Died += Slay;
    }

    private void Slay()
    {
        _enemyDeath.Died -= Slay;
        _slain = true;
    }
}