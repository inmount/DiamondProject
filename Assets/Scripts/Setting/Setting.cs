using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using egg;

public class Setting : MonoBehaviour {

    // 分辨率信息
    public TextMeshProUGUI resolveInfo;
    // 向左
    public Button btnLeft;
    // 向右
    public Button btnRight;
    // 全屏切换
    public Toggle togFullScreen;
    // 返回按钮
    public Button btnBack;

    private List<Vector2> resolves;
    private int resolveIndex;
    // 存储配置文件目录
    private string path;
    private string pathConfig;

    // 上一个分辨率
    private void OnBeforeResolve() {
        // 索引向前移动
        if (resolveIndex <= 0) {
            resolveIndex = resolves.Count - 1;
        } else {
            resolveIndex--;
        }
        resolveInfo.text = $"{resolves[resolveIndex].x}x{resolves[resolveIndex].y}";
        Screen.SetResolution((int)resolves[resolveIndex].x, (int)resolves[resolveIndex].y, togFullScreen.isOn);
        OnSaveConfig();
    }

    // 下一个分辨率
    private void OnNextResolve() {
        // 索引向后移动
        if (resolveIndex >= resolves.Count - 1) {
            resolveIndex = 0;
        } else {
            resolveIndex++;
        }
        resolveInfo.text = $"{resolves[resolveIndex].x}x{resolves[resolveIndex].y}";
        Screen.SetResolution((int)resolves[resolveIndex].x, (int)resolves[resolveIndex].y, togFullScreen.isOn);
        OnSaveConfig();
    }

    // 切换全屏
    private void OnFullScreen(bool value) {
        Screen.SetResolution((int)resolves[resolveIndex].x, (int)resolves[resolveIndex].y, value);
        OnSaveConfig();
    }

    // Start is called before the first frame update
    void Start() {
        // 显示鼠标
        Cursor.visible = true;
        resolves = new List<Vector2>();
        resolves.Add(new Vector2(1920, 1080));
        resolves.Add(new Vector2(1680, 1050));
        resolves.Add(new Vector2(1440, 900));
        resolves.Add(new Vector2(1366, 768));
        resolves.Add(new Vector2(1280, 720));
        resolves.Add(new Vector2(1024, 768));
        resolveIndex = 0;
        // 绑定事件
        btnLeft.onClick.AddListener(OnBeforeResolve);
        btnRight.onClick.AddListener(OnNextResolve);
        togFullScreen.onValueChanged.AddListener(OnFullScreen);
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
        // 读取配置
        OnLoadConfig();
    }

    private void onBackClick() {
        Debug.Log("load 'Main' ...");
        SceneManager.LoadScene("Splash");
    }

    private void OnLoadConfig() {
        // 读取配置文件
        using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
            var doc = cfg.Document;
            resolveIndex = doc["Window"]["Resolve"].ToInteger();
            togFullScreen.isOn = doc["Window"]["FullScreen"].ToInteger() > 0;
            // 显示当前的分辨率信息
            resolveInfo.text = $"{resolves[resolveIndex].x}x{resolves[resolveIndex].y}";
        }
    }

    // 保存设置
    private void OnSaveConfig() {
        // 读取配置文件
        using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
            var doc = cfg.Document;
            doc["Window"]["FullScreen"] = togFullScreen.isOn ? "1" : "0";
            doc["Window"]["Resolve"] = $"{resolveIndex}";
            cfg.Save();
        }
    }
}
