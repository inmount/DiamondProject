using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour {

    // �����ٶ�
    public float speed = 0.5f;
    // ���������ֵ
    public float siteChange = 0.5f;
    // ��ǰ����
    public float siteTarget = 1;

    private Transform trans;

    // Start is called before the first frame update
    void Start() {
        trans = this.transform;
        // �������һ����ʼλ��
        trans.Translate(Vector3.up * Random.Range(siteChange * -1, siteChange), Space.Self);
    }

    // Update is called once per frame
    void Update() {
        // �ƶ�λ��
        trans.Translate(Vector3.up * Time.deltaTime * speed * siteTarget, Space.Self);
        // ��λ���˸ı䷽��
        if (trans.localPosition.y * siteTarget >= siteChange) siteTarget *= -1;
    }
}
