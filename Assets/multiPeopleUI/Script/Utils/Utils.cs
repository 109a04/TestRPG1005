using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    // �Ψ��H���ͦ����a��m��
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-20, 20), 4, Random.Range(-20, 20));
    }
}
