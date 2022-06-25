using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour {

    // 沉浮速度
    public float speed = 0.5f;
    // 沉浮的最大值
    public float siteChange = 0.5f;
    // 当前方向
    public float siteTarget = 1;

    private Transform trans;

    // Start is called before the first frame update
    void Start() {
        trans = this.transform;
        // 随机生成一个初始位置
        trans.Translate(Vector3.up * Random.Range(siteChange * -1, siteChange), Space.Self);
    }

    // Update is called once per frame
    void Update() {
        // 移动位置
        trans.Translate(Vector3.up * Time.deltaTime * speed * siteTarget, Space.Self);
        // 到位置了改变方向
        if (trans.localPosition.y * siteTarget >= siteChange) siteTarget *= -1;
    }
}
