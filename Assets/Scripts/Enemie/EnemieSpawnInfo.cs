using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemieSpawnInfo
{
    // Prefab do inimigo a ser spawnado
    public GameObject enemyPrefab;
    // Posição onde o inimigo será criado
    public Vector2 spawnPosition;
    // Tempo até o inimigo ser spawnado
    public float spawnTime;
    public int difficultLevel;
}
