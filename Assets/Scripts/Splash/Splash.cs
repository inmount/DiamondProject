using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using egg;

public class Splash : MonoBehaviour {

    // 开始游戏
    public Button btnStart;
    // 全球排名
    public Button btnRank;
    // 设置按钮
    public Button btnSetting;
    // 结束游戏
    public Button btnFinish;

    // 存储配置文件目录
    private string path;
    private string pathConfig;
    private List<Vector2> resolves;

    // Start is called before the first frame update
    void Start() {
        // 设置为正常游戏时间
        UnityEngine.Time.timeScale = 1;
        // 显示鼠标
        Cursor.visible = true;
        if (btnStart == null) {
            Debug.LogError("Start button not found!");
            return;
        }
        if (btnRank == null) {
            Debug.LogError("Rank button not found!");
            return;
        }
        if (btnSetting == null) {
            Debug.LogError("Finish button not found!");
            return;
        }
        if (btnFinish == null) {
            Debug.LogError("Finish button not found!");
            return;
        }
        btnStart.onClick.AddListener(onStartClick);
        btnRank.onClick.AddListener(onRankClick);
        btnSetting.onClick.AddListener(onSettingClick);
        btnFinish.onClick.AddListener(onFinishClick);
        // 初始化分辨率列表
        resolves = new List<Vector2>();
        resolves.Add(new Vector2(1920, 1080));
        resolves.Add(new Vector2(1680, 1050));
        resolves.Add(new Vector2(1440, 900));
        resolves.Add(new Vector2(1366, 768));
        resolves.Add(new Vector2(1280, 720));
        resolves.Add(new Vector2(1024, 768));
        // 读取文档目录
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "DiamondPlan";
        // 创建私有文件夹
        eggs.IO.CreateFolder(path);
        // 设置配置文件路径
        pathConfig = path + "\\setting.cfg";
        // 读取配置
        OnLoadConfig();
    }

    private void OnLoadConfig() {
        // 读取配置文件
        using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
            var doc = cfg.Document;
            bool isFullScreen = doc["Window"]["FullScreen"].ToInteger() > 0;
            int resolveIndex = doc["Window"]["Resolve"].ToInteger();
            // 设置当前的分辨率
            Screen.SetResolution((int)resolves[resolveIndex].x, (int)resolves[resolveIndex].y, isFullScreen);
        }
    }

    private void onStartClick() {
        Debug.Log("load 'Main' ...");
        SceneManager.LoadScene("Main");
    }

    private void onRankClick() {
        Debug.Log("load 'Rank' ...");
        SceneManager.LoadScene("Rank");
    }

    private void onSettingClick() {
        Debug.Log("load 'Rank' ...");
        SceneManager.LoadScene("Setting");
    }

    private void onFinishClick() {
        Application.Quit();
    }
}
