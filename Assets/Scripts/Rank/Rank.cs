using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using egg;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Json = egg.Serializable.Json;
using TMPro;

public class Rank : MonoBehaviour {

    //行高
    private const float RowHeight = 50f;

    // 返回按钮
    public Button btnBack;
    // 列表的Content对象
    public GameObject content;
    // 文本对象预制件
    public GameObject textPefab;

    // 存储配置文件目录
    private string path;
    private string pathConfig;
    // 注册信息
    private string uid;

    // Start is called before the first frame update
    void Start() {
        // 显示鼠标
        Cursor.visible = true;
        // 添加返回事件
        btnBack.onClick.AddListener(onBackClick);
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
        }
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
            if (response.Result > 0) {
                userId = response.Data["ID"];
            }
        }
        // 获取数据
        using (egg.CloudDB.Client client = egg.CloudDB.Client.CreateByUser(CloudDBDefine.Api_Url, CloudDBDefine.User_Name, CloudDBDefine.User_Password)) {
            // 定义参数
            Json.Object data = new Json.Object();
            data["Query_Gid"] = CloudDBDefine.Query_GetRankingTop100;
            var response = client.GetResponse("/Query/Result", data);
            Debug.Log(response.ToJsonString());
            // 当成功返回数据时
            if (response.Result > 0) {
                // 获取content组件
                RectTransform contentTransform = content.GetComponent<RectTransform>();
                contentTransform.sizeDelta = new Vector2(0, RowHeight * response.Datas.Count);
                // 遍历所有的结果
                for (int i = 0; i < response.Datas.Count; i++) {
                    var row = response.Datas[i];
                    // 创建序号数据
                    GameObject txtNum = GameObject.Instantiate(textPefab, content.transform);
                    RectTransform txtNumTransform = txtNum.GetComponent<RectTransform>();
                    TextMeshProUGUI txtNumGUI = txtNum.GetComponent<TextMeshProUGUI>();
                    txtNumTransform.SetParent(content.transform, false);
                    // 设置位置
                    txtNumTransform.anchoredPosition = new Vector3(0, 0, 0);
                    txtNumTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
                    txtNumTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RowHeight);
                    txtNumTransform.localPosition = new Vector3(100, 0 - i * RowHeight - RowHeight / 2, 0);
                    // 设置内容
                    txtNumGUI.alignment = TextAlignmentOptions.Center;
                    txtNumGUI.text = $"#{i + 1}";
                    // 创建玩家数据
                    GameObject txtPlayer = GameObject.Instantiate(textPefab, content.transform);
                    RectTransform txtPlayerTransform = txtPlayer.GetComponent<RectTransform>();
                    TextMeshProUGUI txtPlayerGUI = txtPlayer.GetComponent<TextMeshProUGUI>();
                    txtNumTransform.SetParent(content.transform, false);
                    // 设置位置
                    txtPlayerTransform.anchoredPosition = new Vector3(0, 0, 0);
                    txtPlayerTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
                    txtPlayerTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RowHeight);
                    txtPlayerTransform.localPosition = new Vector3(400, 0 - i * RowHeight - RowHeight / 2, 0);
                    // 设置内容
                    if (row["UserID"] == "") {
                        txtPlayerGUI.text = row["Name"];
                    } else {
                        if (row["UserID"] == userId) {
                            txtPlayerGUI.text = row["Name"] + " (我)";
                        } else {
                            txtPlayerGUI.text = row["Name"];
                        }
                    }
                    // 创建得分数据
                    GameObject txtScore = GameObject.Instantiate(textPefab, content.transform);
                    RectTransform txtScoreTransform = txtScore.GetComponent<RectTransform>();
                    TextMeshProUGUI txtScoreGUI = txtScore.GetComponent<TextMeshProUGUI>();
                    txtNumTransform.SetParent(content.transform, false);
                    // 设置位置
                    txtScoreTransform.anchoredPosition = new Vector3(0, 0, 0);
                    txtScoreTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
                    txtScoreTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RowHeight);
                    txtScoreTransform.localPosition = new Vector3(700, 0 - i * RowHeight - RowHeight / 2, 0);
                    // 设置内容
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
