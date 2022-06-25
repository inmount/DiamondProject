using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Body : MonoBehaviour {
    // 动画组件
    private Animation jump;
    // 身体信息
    private BodyInfo bodyInfo;
    // 声音组件
    private AudioSource[] jumpAudio;
    // 声音组件
    private AudioSource[] musicAudio;

    // Start is called before the first frame update
    void Start() {
        // 获取动画组件
        jump = this.GetComponent<Animation>();
        // 获取身体信息
        bodyInfo = this.GetComponent<BodyInfo>();
        // 获取声音组件
        jumpAudio = this.GetComponents<AudioSource>();
        // 获取父对象声音信息
        musicAudio = this.GetComponentsInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        // 按ESC键游戏暂停或继续
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
        // 按空格键或向上键
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) {
            if (!jump.isPlaying) {
                jumpAudio[0].Play();
                jump.Play("jump");
            }
        }
    }
}
