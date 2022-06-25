using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // �������е�λ����Ϣ
    private float[] sites;

    /// <summary>
    /// ���λ��
    /// </summary>
    public int Site;

    // ��������
    private BodyInfo bodyInfo;

    // Start is called before the first frame update
    void Start() {
        // ��ʾ���
        Cursor.visible = false;
        // ��ʼ��λ����Ϣ
        sites = new float[] { -3.75f, -1.25f, 1.25f, 3.75f };
        // �������һ��λ��
        this.Site = (int)(sites.Length * Random.value);
        // ������ҳ�ʼλ��
        this.transform.position = new Vector3(sites[this.Site], this.transform.position.y, this.transform.position.z);
        // ��ȡ��������
        bodyInfo = this.GetComponentInChildren<BodyInfo>();
    }

    // Update is called once per frame
    void Update() {
        // �ж��Ƿ�����Ϸ��
        if (bodyInfo.gamming) {
            // �����
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (this.Site > 0) {
                    this.Site--;
                    this.transform.Translate(Vector3.left * 2.5f);
                }
            }
            // �ҷ����
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (this.Site < 3) {
                    this.Site++;
                    this.transform.Translate(Vector3.right * 2.5f);
                }
            }
        }
    }
}
