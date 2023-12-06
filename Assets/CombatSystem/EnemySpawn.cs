using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制怪物生成的腳本
/// </summary>

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn Instance;
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    private void Awake()
    {
        Instance = this;
    }

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
