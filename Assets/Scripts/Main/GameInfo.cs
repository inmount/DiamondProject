using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using egg;

public class GameInfo : MonoBehaviour {
    // 身体信息
    private BodyInfo bodyInfo;
    public GameObject body;
    // 速度信息 
    private AutoMove autoMove;
    // 文本框
    public TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start() {
        if (body == null) {
            Debug.LogError("Body object not found.");
            return;
        }
        if (textMeshPro == null) {
            Debug.LogError("Text object not found.");
            return;
        }
        bodyInfo = body.GetComponent<BodyInfo>();
        autoMove = body.GetComponentInParent<AutoMove>();
    }

    // Update is called once per frame
    void Update() {
        if (bodyInfo.gamming) {
            if (UnityEngine.Time.timeScale == 0) {
                textMeshPro.text = $"速度 {autoMove.speed.ToString("F1")} 得分 {bodyInfo.score} 暂停";
            } else {
                textMeshPro.text = $"速度 {autoMove.speed.ToString("F1")} 得分 {bodyInfo.score}";
            }
        } else {
            textMeshPro.text = $"游戏结束 得分 {bodyInfo.score} 按ESC退出";
        }
    }
}
