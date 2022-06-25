using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using egg;

public class GameInfo : MonoBehaviour {
    // ������Ϣ
    private BodyInfo bodyInfo;
    public GameObject body;
    // �ٶ���Ϣ 
    private AutoMove autoMove;
    // �ı���
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
                textMeshPro.text = $"�ٶ� {autoMove.speed.ToString("F1")} �÷� {bodyInfo.score} ��ͣ";
            } else {
                textMeshPro.text = $"�ٶ� {autoMove.speed.ToString("F1")} �÷� {bodyInfo.score}";
            }
        } else {
            textMeshPro.text = $"��Ϸ���� �÷� {bodyInfo.score} ��ESC�˳�";
        }
    }
}
