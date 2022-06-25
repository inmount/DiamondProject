using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyInfo : MonoBehaviour {

    /// <summary>
    /// 得分
    /// </summary>
    public int score = 0;

    /// <summary>
    /// 游戏是否进行中
    /// </summary>
    public bool gamming = true;

    private void Awake() {
        score = 0;
        gamming = true;
    }

}
