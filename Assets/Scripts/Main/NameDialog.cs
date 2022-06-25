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

    // 输入框
    public TMP_InputField txtField;
    // 提交按钮
    public Button btnSubmit;
    // 身体对象
    public GameObject body;

    // 存储配置文件目录
    private string path;
    private string pathConfig;
    // 身体信息
    private BodyInfo bodyInfo;
    // 注册信息
    private string uid;

    // Start is called before the first frame update
    void Start() {
        // 绑定按钮事件
        btnSubmit.interactable = true;
        btnSubmit.onClick.AddListener(OnSubmitClick);
        // 获取身体信息
        bodyInfo = body.GetComponent<BodyInfo>();
        // 读取文档目录
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "DiamondPlan";
        // 创建私有文件夹
        eggs.IO.CreateFolder(path);
        // 设置配置文件路径
        pathConfig = path + "\\setting.cfg";
        // 读取配置文件
        using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
            var doc = cfg.Document;
            uid = doc["User"]["Uid"];
            txtField.text = doc["User"]["Name"];
        }
    }

    // 提交点击
    private void OnSubmitClick() {
        // 设定按钮不可用
        btnSubmit.interactable = false;
        // 获取输入的名称
        string name = txtField.text.Trim()
            .Replace("'", "").Replace("\"", "")
            .Replace("(", "").Replace(")", "")
            .Replace("[", "").Replace("]", "")
            .Replace("{", "").Replace("}", "");
        if (name.IsEmpty()) name = "无名";
        try {
            // 定义用户信息
            long userId = 0;
            // 检测uid有效性，并获取UserId，无效则新注册一个
            using (egg.CloudDB.Client client = egg.CloudDB.Client.CreateByUser(CloudDBDefine.Api_Url, CloudDBDefine.User_Name, CloudDBDefine.User_Password)) {
                // 查找用户
                Json.Object data = new Json.Object();
                data["Table_Gid"] = CloudDBDefine.Table_UserInfo;
                data["Field"] = "Uid";
                data["Value"] = uid;
                var response = client.GetResponse("/Data/Find", data);
                Debug.Log(response.ToJsonString());
                // 用户不存在，则添加新用户
                if (response.Result <= 0) {
                    data = new Json.Object();
                    data["Table_Gid"] = CloudDBDefine.Table_UserInfo;
                    data["Name"] = "";
                    data["Password"] = "";
                    data["Nick"] = name;
                    response = client.GetResponse("/Data/Add", data);
                    Debug.Log(response.ToJsonString());
                    uid = response.Data["Uid"];
                    // 重新获取数据
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
            // 存储到配置文件
            using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
                var doc = cfg.Document;
                doc["User"]["Uid"] = uid;
                doc["User"]["Name"] = name;
                cfg.Save();
            }
            // 提交到服务器
            using (egg.CloudDB.Client client = egg.CloudDB.Client.CreateByUser(CloudDBDefine.Api_Url, CloudDBDefine.User_Name, CloudDBDefine.User_Password)) {
                // 定义参数
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
        // 跳转到排名场景
        SceneManager.LoadScene("Rank");
    }

}
