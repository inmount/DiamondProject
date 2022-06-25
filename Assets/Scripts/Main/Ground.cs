using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    /// <summary>
    /// ����·��Ԥ�Ƽ�
    /// </summary>
    public GameObject roadPefab;

    /// <summary>
    /// ����ʯͷԤ�Ƽ�
    /// </summary>
    public GameObject stonePefab;

    /// <summary>
    /// ����ǽ��Ԥ�Ƽ�
    /// </summary>
    public GameObject[] wallPefabs;

    /// <summary>
    /// ������ʯԤ�Ƽ�
    /// </summary>
    public GameObject diamondPefab;

    // �洢���еĳ�������
    private List<GameObject> gameObjects;

    // �������λ��
    public float createZ;
    // ������ĳߴ�
    private float rectSize = 20;

    // ����һ������
    public void CreateRect() {
        // ������������
        var rect = new GameObject("Rect_" + createZ);
        rect.transform.position = new Vector3(0, 0, createZ);
        rect.transform.parent = this.transform;
        // ��������ӵ���������
        gameObjects.Add(rect);
        // �����ذ�
        var plane = GameObject.Instantiate(roadPefab, rect.transform);
        // ���õذ峤��
        //plane.transform.localScale = new Vector3(1f, 1f, rectSize / 10f);
        // ����ǽ��1�ǰ�ǽ��2�Ǹ�ǽ
        int[] walls = new int[4];
        for (int i = 0; i < walls.Length; i++) {
            walls[i] = Random.Range(1, 3);
        }
        // �������һ����ȱ
        walls[Random.Range(0, walls.Length)] = 0;
        // ����ǽ��
        for (int i = 0; i < walls.Length; i++) {
            switch (walls[i]) {
                case 1: // ��ǽ
                    var wall1 = GameObject.Instantiate(stonePefab, rect.transform);
                    wall1.name = "wall";
                    wall1.transform.localPosition = new Vector3((i - 2) * 2.5f + 1.25f, 0.0f, -3);
                    break;
                case 2: // ��ǽ
                    var wall2 = GameObject.Instantiate(wallPefabs[Random.Range(0, wallPefabs.Length)], rect.transform);
                    wall2.name = "wall";
                    wall2.transform.localPosition = new Vector3((i - 2) * 2.5f + 1.25f, 0f, -3);
                    //wall2.transform.localScale = new Vector3(2.3f, 2.2f, 0.8f);
                    break;
            }
        }
        // ���ɵ÷���ʯ
        int diamondSite = Random.Range(0, 4);
        var diamond = GameObject.Instantiate(diamondPefab, rect.transform);
        diamond.name = "diamond";
        diamond.transform.localPosition = new Vector3((diamondSite - 2) * 2.5f + 1.25f, 1.0f, 3);
        // ����ǰ��
        createZ += rectSize;
    }

    // Start is called before the first frame update
    void Start() {
        // �ж�Ԥ�Ƽ��ǲ����Ѿ���ֵ
        if (roadPefab == null || wallPefabs == null || diamondPefab == null) {
            Debug.Log("����Ԥ�Ƽ�ȱʧ");
            return;
        }
        // �������������б�
        gameObjects = new List<GameObject>();
        // ��ʼ������λ��
        createZ = 20;
        // ������������10���������൱��100��
        for (int i = 0; i < 10; i++) {
            // ����һ������
            CreateRect();
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
