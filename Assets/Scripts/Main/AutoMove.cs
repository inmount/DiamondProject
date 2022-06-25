using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    // 设置速度
    public float speed;
    // 设置加速时间
    private float speedPlusTime = 5;
    // 设置最大最小速度
    public float speedMin = 5;
    public float speedMax = 20;
    // 计时器
    private float timeSpeedNext = 0;
    // 对象
    private Transform trans;
    // Ground对象
    private Ground ground;

    // Start is called before the first frame update
    void Start() {
        // 存储对象引用
        trans = this.transform;
        // 获取场景对象
        ground = Object.FindObjectOfType<Ground>();
        // 设置初始速度
        speed = speedMin;
        // 默认自动开始游戏
        Time.timeScale = 1;
        timeSpeedNext = Time.time + speedPlusTime;
    }

    // Update is called once per frame
    void Update() {
        // 获取当前时间间隔
        float dt = Time.deltaTime;
        // 向前移动
        trans.Translate(Vector3.forward * speed * dt);
        // 执行加速
        if (Time.time >= timeSpeedNext && speed < speedMax) {
            timeSpeedNext = Time.time + speedPlusTime;
            // 加速越来慢
            speed = Mathf.Lerp(speed, speedMax, 0.05f);
            if (speedMax - speed < 0.01) speed = speedMax;
        }
        // 根据距离生成新的场景
        if (ground.createZ - trans.position.z < 100) ground.CreateRect();
    }
}
