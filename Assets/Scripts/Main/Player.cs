using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // 定义所有的位置信息
    private float[] sites;

    /// <summary>
    /// 玩家位置
    /// </summary>
    public int Site;

    // 身体数据
    private BodyInfo bodyInfo;

    // Start is called before the first frame update
    void Start() {
        // 显示鼠标
        Cursor.visible = false;
        // 初始化位置信息
        sites = new float[] { -3.75f, -1.25f, 1.25f, 3.75f };
        // 随机生成一个位置
        this.Site = (int)(sites.Length * Random.value);
        // 设置玩家初始位置
        this.transform.position = new Vector3(sites[this.Site], this.transform.position.y, this.transform.position.z);
        // 获取身体数据
        bodyInfo = this.GetComponentInChildren<BodyInfo>();
    }

    // Update is called once per frame
    void Update() {
        // 判断是否在游戏中
        if (bodyInfo.gamming) {
            // 左方向键
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (this.Site > 0) {
                    this.Site--;
                    this.transform.Translate(Vector3.left * 2.5f);
                }
            }
            // 右方向键
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (this.Site < 3) {
                    this.Site++;
                    this.transform.Translate(Vector3.right * 2.5f);
                }
            }
        }
    }
}
