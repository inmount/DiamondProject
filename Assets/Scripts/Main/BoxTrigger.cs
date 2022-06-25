using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour {

    // 身体信息
    private BodyInfo info;

    // 移动信息
    private AutoMove move;

    // 对话框
    public GameObject dig;

    // 声音组件
    private AudioSource[] diamondAudio;

    // 声音组件
    private AudioSource[] musicAudio;

    // 开始
    private void Start() {
        // 获取主角状态
        info = this.GetComponent<BodyInfo>();
        // 获取移动信息
        move = this.GetComponentInParent<AutoMove>();
        // 获取父对象声音信息
        musicAudio = this.GetComponentsInParent<AudioSource>();
        Debug.Log($"BoxTrigger.musicAudio:count({musicAudio.Length})");
        // 获取声音组件
        diamondAudio = this.GetComponents<AudioSource>();
        // 初始化对话框
        dig.SetActive(false);
    }

    // 游戏结束
    private void GameOver() {
        // 显示鼠标
        Cursor.visible = true;
        // 停止游戏时间
        Time.timeScale = 0;
        // 背景音乐暂停
        musicAudio[2].Pause();
        // 设置游戏标志
        info.gamming = false;
        // 弹出对话框
        dig.SetActive(true);
    }

    // 碰撞触发
    private void OnTriggerEnter(Collider other) {
        // Debug.Log(other.name);
        // 碰到墙壁，游戏结束
        if (other.name == "wall") {
            // 游戏结束
            GameOver();
        }
        // 碰到钻石，得分加1，钻石消失
        if (other.name == "diamond") {
            Debug.Log($"[{Time.time}] Eat diamond ...");
            diamondAudio[1].Play();
            Destroy(other.gameObject);
            info.score += (int)move.speed;
        }
    }
}
