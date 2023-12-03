using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制怪物生成的腳本
/// </summary>

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMonster()
    {
        if (enemyPrefab == null) return;
        if (spawnPoint == null) return;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
