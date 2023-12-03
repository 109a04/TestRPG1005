using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public Vector3 movementInput;
    // 專門接收使用者輸入的，有需要的話可以再多宣告幾個變數
    // 目前是只有一個移動的變數
}
