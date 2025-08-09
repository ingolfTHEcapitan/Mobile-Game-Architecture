using System;
using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;

[RequireComponent(typeof(UniqueId))]
public class EnemySpawner : MonoBehaviour, ISavedProgress
{
    public EnemyesTypeId EnemyesTypeId;
    public bool Slain;

    private string _id;

    private void Awake() =>
        _id = GetComponent<UniqueId>().Id;

    public void LoadProgress(PlayerProgress progress)
    {
        if (progress.KillData.SlainSpawners.Contains(_id))
            Slain = true;
        else
            Spawn();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        if (Slain)
            progress.KillData.SlainSpawners.Add(_id);
    }
    
    private void Spawn()
    {
    }
}