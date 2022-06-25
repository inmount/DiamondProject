using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    // �����ٶ�
    public float speed;
    // ���ü���ʱ��
    private float speedPlusTime = 5;
    // ���������С�ٶ�
    public float speedMin = 5;
    public float speedMax = 20;
    // ��ʱ��
    private float timeSpeedNext = 0;
    // ����
    private Transform trans;
    // Ground����
    private Ground ground;

    // Start is called before the first frame update
    void Start() {
        // �洢��������
        trans = this.transform;
        // ��ȡ��������
        ground = Object.FindObjectOfType<Ground>();
        // ���ó�ʼ�ٶ�
        speed = speedMin;
        // Ĭ���Զ���ʼ��Ϸ
        Time.timeScale = 1;
        timeSpeedNext = Time.time + speedPlusTime;
    }

    // Update is called once per frame
    void Update() {
        // ��ȡ��ǰʱ����
        float dt = Time.deltaTime;
        // ��ǰ�ƶ�
        trans.Translate(Vector3.forward * speed * dt);
        // ִ�м���
        if (Time.time >= timeSpeedNext && speed < speedMax) {
            timeSpeedNext = Time.time + speedPlusTime;
            // ����Խ����
            speed = Mathf.Lerp(speed, speedMax, 0.05f);
            if (speedMax - speed < 0.01) speed = speedMax;
        }
        // ���ݾ��������µĳ���
        if (ground.createZ - trans.position.z < 100) ground.CreateRect();
    }
}
