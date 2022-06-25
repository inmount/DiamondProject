using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    /// <summary>
    /// 定义路面预制件
    /// </summary>
    public GameObject roadPefab;

    /// <summary>
    /// 定义石头预制件
    /// </summary>
    public GameObject stonePefab;

    /// <summary>
    /// 定义墙面预制件
    /// </summary>
    public GameObject[] wallPefabs;

    /// <summary>
    /// 定义钻石预制件
    /// </summary>
    public GameObject diamondPefab;

    // 存储所有的场景对象
    private List<GameObject> gameObjects;

    // 新区域的位置
    public float createZ;
    // 新区域的尺寸
    private float rectSize = 20;

    // 创建一个区域
    public void CreateRect() {
        // 创建区域容器
        var rect = new GameObject("Rect_" + createZ);
        rect.transform.position = new Vector3(0, 0, createZ);
        rect.transform.parent = this.transform;
        // 将区域添加到场景对象
        gameObjects.Add(rect);
        // 创建地板
        var plane = GameObject.Instantiate(roadPefab, rect.transform);
        // 设置地板长度
        //plane.transform.localScale = new Vector3(1f, 1f, rectSize / 10f);
        // 创建墙，1是矮墙，2是高墙
        int[] walls = new int[4];
        for (int i = 0; i < walls.Length; i++) {
            walls[i] = Random.Range(1, 3);
        }
        // 随机生成一个空缺
        walls[Random.Range(0, walls.Length)] = 0;
        // 生成墙壁
        for (int i = 0; i < walls.Length; i++) {
            switch (walls[i]) {
                case 1: // 矮墙
                    var wall1 = GameObject.Instantiate(stonePefab, rect.transform);
                    wall1.name = "wall";
                    wall1.transform.localPosition = new Vector3((i - 2) * 2.5f + 1.25f, 0.0f, -3);
                    break;
                case 2: // 高墙
                    var wall2 = GameObject.Instantiate(wallPefabs[Random.Range(0, wallPefabs.Length)], rect.transform);
                    wall2.name = "wall";
                    wall2.transform.localPosition = new Vector3((i - 2) * 2.5f + 1.25f, 0f, -3);
                    //wall2.transform.localScale = new Vector3(2.3f, 2.2f, 0.8f);
                    break;
            }
        }
        // 生成得分钻石
        int diamondSite = Random.Range(0, 4);
        var diamond = GameObject.Instantiate(diamondPefab, rect.transform);
        diamond.name = "diamond";
        diamond.transform.localPosition = new Vector3((diamondSite - 2) * 2.5f + 1.25f, 1.0f, 3);
        // 区域前推
        createZ += rectSize;
    }

    // Start is called before the first frame update
    void Start() {
        // 判断预制件是不是已经赋值
        if (roadPefab == null || wallPefabs == null || diamondPefab == null) {
            Debug.Log("场景预制件缺失");
            return;
        }
        // 建立场景对象列表
        gameObjects = new List<GameObject>();
        // 初始化区域位置
        createZ = 20;
        // 进场景先生成10个部件，相当于100米
        for (int i = 0; i < 10; i++) {
            // 创建一个区域
            CreateRect();
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
