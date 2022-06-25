using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using egg;

public class Splash : MonoBehaviour {

    // ��ʼ��Ϸ
    public Button btnStart;
    // ȫ������
    public Button btnRank;
    // ���ð�ť
    public Button btnSetting;
    // ������Ϸ
    public Button btnFinish;

    // �洢�����ļ�Ŀ¼
    private string path;
    private string pathConfig;
    private List<Vector2> resolves;

    // Start is called before the first frame update
    void Start() {
        // ����Ϊ������Ϸʱ��
        UnityEngine.Time.timeScale = 1;
        // ��ʾ���
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
        // ��ʼ���ֱ����б�
        resolves = new List<Vector2>();
        resolves.Add(new Vector2(1920, 1080));
        resolves.Add(new Vector2(1680, 1050));
        resolves.Add(new Vector2(1440, 900));
        resolves.Add(new Vector2(1366, 768));
        resolves.Add(new Vector2(1280, 720));
        resolves.Add(new Vector2(1024, 768));
        // ��ȡ�ĵ�Ŀ¼
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "DiamondPlan";
        // ����˽���ļ���
        eggs.IO.CreateFolder(path);
        // ���������ļ�·��
        pathConfig = path + "\\setting.cfg";
        // ��ȡ����
        OnLoadConfig();
    }

    private void OnLoadConfig() {
        // ��ȡ�����ļ�
        using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
            var doc = cfg.Document;
            bool isFullScreen = doc["Window"]["FullScreen"].ToInteger() > 0;
            int resolveIndex = doc["Window"]["Resolve"].ToInteger();
            // ���õ�ǰ�ķֱ���
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
