using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyInfo : MonoBehaviour {

    /// <summary>
    /// �÷�
    /// </summary>
    public int score = 0;

    /// <summary>
    /// ��Ϸ�Ƿ������
    /// </summary>
    public bool gamming = true;

    private void Awake() {
        score = 0;
        gamming = true;
    }

}
