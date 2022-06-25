using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Body : MonoBehaviour {
    // �������
    private Animation jump;
    // ������Ϣ
    private BodyInfo bodyInfo;
    // �������
    private AudioSource[] jumpAudio;
    // �������
    private AudioSource[] musicAudio;

    // Start is called before the first frame update
    void Start() {
        // ��ȡ�������
        jump = this.GetComponent<Animation>();
        // ��ȡ������Ϣ
        bodyInfo = this.GetComponent<BodyInfo>();
        // ��ȡ�������
        jumpAudio = this.GetComponents<AudioSource>();
        // ��ȡ������������Ϣ
        musicAudio = this.GetComponentsInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        // ��ESC����Ϸ��ͣ�����
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (bodyInfo.gamming) {
                if (Time.timeScale > 0) {
                    musicAudio[2].Pause();
                    Time.timeScale = 0;
                } else {
                    musicAudio[2].Play();
                    Time.timeScale = 1;
                }
            } else {
                SceneManager.LoadScene("Splash");
            }
        }
        // ���ո�������ϼ�
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) {
            if (!jump.isPlaying) {
                jumpAudio[0].Play();
                jump.Play("jump");
            }
        }
    }
}
