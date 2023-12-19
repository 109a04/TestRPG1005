using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    // 用來隨機生成玩家位置的
    public static Vector3 GetRandomSpawnPoint()
    {
        Debug.Log("隨機");
        return new Vector3(Random.Range(-20, 20), 4, Random.Range(-20, 20));
    }
}
