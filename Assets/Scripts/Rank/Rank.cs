using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using egg;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Json = egg.Serializable.Json;
using TMPro;

public class Rank : MonoBehaviour {

    //�и�
    private const float RowHeight = 50f;

    // ���ذ�ť
    public Button btnBack;
    // �б��Content����
    public GameObject content;
    // �ı�����Ԥ�Ƽ�
    public GameObject textPefab;

    // �洢�����ļ�Ŀ¼
    private string path;
    private string pathConfig;
    // ע����Ϣ
    private string uid;

    // Start is called before the first frame update
    void Start() {
        // ��ʾ���
        Cursor.visible = true;
        // ��ӷ����¼�
        btnBack.onClick.AddListener(onBackClick);
        // ��ȡ�ĵ�Ŀ¼
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "DiamondPlan";
        // ����˽���ļ���
        eggs.IO.CreateFolder(path);
        // ���������ļ�·��
        pathConfig = path + "\\setting.cfg";
        // ��ȡ�����ļ�
        using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
            var doc = cfg.Document;
            uid = doc["User"]["Uid"];
        }
        // �����û���Ϣ
        long userId = 0;
        // ���uid��Ч�ԣ�����ȡUserId����Ч����ע��һ��
        using (egg.CloudDB.Client client = egg.CloudDB.Client.CreateByUser(CloudDBDefine.Api_Url, CloudDBDefine.User_Name, CloudDBDefine.User_Password)) {
            // �����û�
            Json.Object data = new Json.Object();
            data["Table_Gid"] = CloudDBDefine.Table_UserInfo;
            data["Field"] = "Uid";
            data["Value"] = uid;
            var response = client.GetResponse("/Data/Find", data);
            Debug.Log(response.ToJsonString());
            if (response.Result > 0) {
                userId = response.Data["ID"];
            }
        }
        // ��ȡ����
        using (egg.CloudDB.Client client = egg.CloudDB.Client.CreateByUser(CloudDBDefine.Api_Url, CloudDBDefine.User_Name, CloudDBDefine.User_Password)) {
            // �������
            Json.Object data = new Json.Object();
            data["Query_Gid"] = CloudDBDefine.Query_GetRankingTop100;
            var response = client.GetResponse("/Query/Result", data);
            Debug.Log(response.ToJsonString());
            // ���ɹ���������ʱ
            if (response.Result > 0) {
                // ��ȡcontent���
                RectTransform contentTransform = content.GetComponent<RectTransform>();
                contentTransform.sizeDelta = new Vector2(0, RowHeight * response.Datas.Count);
                // �������еĽ��
                for (int i = 0; i < response.Datas.Count; i++) {
                    var row = response.Datas[i];
                    // �����������
                    GameObject txtNum = GameObject.Instantiate(textPefab, content.transform);
                    RectTransform txtNumTransform = txtNum.GetComponent<RectTransform>();
                    TextMeshProUGUI txtNumGUI = txtNum.GetComponent<TextMeshProUGUI>();
                    txtNumTransform.SetParent(content.transform, false);
                    // ����λ��
                    txtNumTransform.anchoredPosition = new Vector3(0, 0, 0);
                    txtNumTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
                    txtNumTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RowHeight);
                    txtNumTransform.localPosition = new Vector3(100, 0 - i * RowHeight - RowHeight / 2, 0);
                    // ��������
                    txtNumGUI.alignment = TextAlignmentOptions.Center;
                    txtNumGUI.text = $"#{i + 1}";
                    // �����������
                    GameObject txtPlayer = GameObject.Instantiate(textPefab, content.transform);
                    RectTransform txtPlayerTransform = txtPlayer.GetComponent<RectTransform>();
                    TextMeshProUGUI txtPlayerGUI = txtPlayer.GetComponent<TextMeshProUGUI>();
                    txtNumTransform.SetParent(content.transform, false);
                    // ����λ��
                    txtPlayerTransform.anchoredPosition = new Vector3(0, 0, 0);
                    txtPlayerTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
                    txtPlayerTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RowHeight);
                    txtPlayerTransform.localPosition = new Vector3(400, 0 - i * RowHeight - RowHeight / 2, 0);
                    // ��������
                    if (row["UserID"] == "") {
                        txtPlayerGUI.text = row["Name"];
                    } else {
                        if (row["UserID"] == userId) {
                            txtPlayerGUI.text = row["Name"] + " (��)";
                        } else {
                            txtPlayerGUI.text = row["Name"];
                        }
                    }
                    // �����÷�����
                    GameObject txtScore = GameObject.Instantiate(textPefab, content.transform);
                    RectTransform txtScoreTransform = txtScore.GetComponent<RectTransform>();
                    TextMeshProUGUI txtScoreGUI = txtScore.GetComponent<TextMeshProUGUI>();
                    txtNumTransform.SetParent(content.transform, false);
                    // ����λ��
                    txtScoreTransform.anchoredPosition = new Vector3(0, 0, 0);
                    txtScoreTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
                    txtScoreTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RowHeight);
                    txtScoreTransform.localPosition = new Vector3(700, 0 - i * RowHeight - RowHeight / 2, 0);
                    // ��������
                    txtScoreGUI.alignment = TextAlignmentOptions.Center;
                    txtScoreGUI.text = row["Score"];
                }
            }
        }
    }

    private void onBackClick() {
        Debug.Log("load 'Main' ...");
        SceneManager.LoadScene("Splash");
    }
}
