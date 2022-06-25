using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour {

    // ������Ϣ
    private BodyInfo info;

    // �ƶ���Ϣ
    private AutoMove move;

    // �Ի���
    public GameObject dig;

    // �������
    private AudioSource[] diamondAudio;

    // �������
    private AudioSource[] musicAudio;

    // ��ʼ
    private void Start() {
        // ��ȡ����״̬
        info = this.GetComponent<BodyInfo>();
        // ��ȡ�ƶ���Ϣ
        move = this.GetComponentInParent<AutoMove>();
        // ��ȡ������������Ϣ
        musicAudio = this.GetComponentsInParent<AudioSource>();
        Debug.Log($"BoxTrigger.musicAudio:count({musicAudio.Length})");
        // ��ȡ�������
        diamondAudio = this.GetComponents<AudioSource>();
        // ��ʼ���Ի���
        dig.SetActive(false);
    }

    // ��Ϸ����
    private void GameOver() {
        // ��ʾ���
        Cursor.visible = true;
        // ֹͣ��Ϸʱ��
        Time.timeScale = 0;
        // ����������ͣ
        musicAudio[2].Pause();
        // ������Ϸ��־
        info.gamming = false;
        // �����Ի���
        dig.SetActive(true);
    }

    // ��ײ����
    private void OnTriggerEnter(Collider other) {
        // Debug.Log(other.name);
        // ����ǽ�ڣ���Ϸ����
        if (other.name == "wall") {
            // ��Ϸ����
            GameOver();
        }
        // ������ʯ���÷ּ�1����ʯ��ʧ
        if (other.name == "diamond") {
            Debug.Log($"[{Time.time}] Eat diamond ...");
            diamondAudio[1].Play();
            Destroy(other.gameObject);
            info.score += (int)move.speed;
        }
    }
}
