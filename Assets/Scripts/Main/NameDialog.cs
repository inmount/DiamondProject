using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using egg;
using TMPro;
using UnityEngine.UI;
using Json = egg.Serializable.Json;
using UnityEngine.SceneManagement;
using System;

public class NameDialog : MonoBehaviour {

    // �����
    public TMP_InputField txtField;
    // �ύ��ť
    public Button btnSubmit;
    // �������
    public GameObject body;

    // �洢�����ļ�Ŀ¼
    private string path;
    private string pathConfig;
    // ������Ϣ
    private BodyInfo bodyInfo;
    // ע����Ϣ
    private string uid;

    // Start is called before the first frame update
    void Start() {
        // �󶨰�ť�¼�
        btnSubmit.interactable = true;
        btnSubmit.onClick.AddListener(OnSubmitClick);
        // ��ȡ������Ϣ
        bodyInfo = body.GetComponent<BodyInfo>();
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
            txtField.text = doc["User"]["Name"];
        }
    }

    // �ύ���
    private void OnSubmitClick() {
        // �趨��ť������
        btnSubmit.interactable = false;
        // ��ȡ���������
        string name = txtField.text.Trim()
            .Replace("'", "").Replace("\"", "")
            .Replace("(", "").Replace(")", "")
            .Replace("[", "").Replace("]", "")
            .Replace("{", "").Replace("}", "");
        if (name.IsEmpty()) name = "����";
        try {
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
                // �û������ڣ���������û�
                if (response.Result <= 0) {
                    data = new Json.Object();
                    data["Table_Gid"] = CloudDBDefine.Table_UserInfo;
                    data["Name"] = "";
                    data["Password"] = "";
                    data["Nick"] = name;
                    response = client.GetResponse("/Data/Add", data);
                    Debug.Log(response.ToJsonString());
                    uid = response.Data["Uid"];
                    // ���»�ȡ����
                    data = new Json.Object();
                    data["Table_Gid"] = CloudDBDefine.Table_UserInfo;
                    data["Field"] = "Uid";
                    data["Value"] = uid;
                    response = client.GetResponse("/Data/Find", data);
                    Debug.Log(response.ToJsonString());
                    if (response.Result > 0) {
                        userId = response.Data["ID"];
                    }                    
                } else {
                    userId = response.Data["ID"];
                }
            }
            // �洢�������ļ�
            using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
                var doc = cfg.Document;
                doc["User"]["Uid"] = uid;
                doc["User"]["Name"] = name;
                cfg.Save();
            }
            // �ύ��������
            using (egg.CloudDB.Client client = egg.CloudDB.Client.CreateByUser(CloudDBDefine.Api_Url, CloudDBDefine.User_Name, CloudDBDefine.User_Password)) {
                // �������
                Json.Object data = new Json.Object();
                data["Table_Gid"] = CloudDBDefine.Table_Ranking;
                data["UserID"] = userId;
                data["Name"] = name;
                data["Score"] = bodyInfo.score;
                var response = client.GetResponse("/Data/Add", data);
                Debug.Log(response.ToJsonString());
            }
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
        }
        // ��ת����������
        SceneManager.LoadScene("Rank");
    }

}
