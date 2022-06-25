using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using egg;

public class Setting : MonoBehaviour {

    // �ֱ�����Ϣ
    public TextMeshProUGUI resolveInfo;
    // ����
    public Button btnLeft;
    // ����
    public Button btnRight;
    // ȫ���л�
    public Toggle togFullScreen;
    // ���ذ�ť
    public Button btnBack;

    private List<Vector2> resolves;
    private int resolveIndex;
    // �洢�����ļ�Ŀ¼
    private string path;
    private string pathConfig;

    // ��һ���ֱ���
    private void OnBeforeResolve() {
        // ������ǰ�ƶ�
        if (resolveIndex <= 0) {
            resolveIndex = resolves.Count - 1;
        } else {
            resolveIndex--;
        }
        resolveInfo.text = $"{resolves[resolveIndex].x}x{resolves[resolveIndex].y}";
        Screen.SetResolution((int)resolves[resolveIndex].x, (int)resolves[resolveIndex].y, togFullScreen.isOn);
        OnSaveConfig();
    }

    // ��һ���ֱ���
    private void OnNextResolve() {
        // ��������ƶ�
        if (resolveIndex >= resolves.Count - 1) {
            resolveIndex = 0;
        } else {
            resolveIndex++;
        }
        resolveInfo.text = $"{resolves[resolveIndex].x}x{resolves[resolveIndex].y}";
        Screen.SetResolution((int)resolves[resolveIndex].x, (int)resolves[resolveIndex].y, togFullScreen.isOn);
        OnSaveConfig();
    }

    // �л�ȫ��
    private void OnFullScreen(bool value) {
        Screen.SetResolution((int)resolves[resolveIndex].x, (int)resolves[resolveIndex].y, value);
        OnSaveConfig();
    }

    // Start is called before the first frame update
    void Start() {
        // ��ʾ���
        Cursor.visible = true;
        resolves = new List<Vector2>();
        resolves.Add(new Vector2(1920, 1080));
        resolves.Add(new Vector2(1680, 1050));
        resolves.Add(new Vector2(1440, 900));
        resolves.Add(new Vector2(1366, 768));
        resolves.Add(new Vector2(1280, 720));
        resolves.Add(new Vector2(1024, 768));
        resolveIndex = 0;
        // ���¼�
        btnLeft.onClick.AddListener(OnBeforeResolve);
        btnRight.onClick.AddListener(OnNextResolve);
        togFullScreen.onValueChanged.AddListener(OnFullScreen);
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
        // ��ȡ����
        OnLoadConfig();
    }

    private void onBackClick() {
        Debug.Log("load 'Main' ...");
        SceneManager.LoadScene("Splash");
    }

    private void OnLoadConfig() {
        // ��ȡ�����ļ�
        using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
            var doc = cfg.Document;
            resolveIndex = doc["Window"]["Resolve"].ToInteger();
            togFullScreen.isOn = doc["Window"]["FullScreen"].ToInteger() > 0;
            // ��ʾ��ǰ�ķֱ�����Ϣ
            resolveInfo.text = $"{resolves[resolveIndex].x}x{resolves[resolveIndex].y}";
        }
    }

    // ��������
    private void OnSaveConfig() {
        // ��ȡ�����ļ�
        using (var cfg = eggs.IO.OpenConfigDocument(pathConfig)) {
            var doc = cfg.Document;
            doc["Window"]["FullScreen"] = togFullScreen.isOn ? "1" : "0";
            doc["Window"]["Resolve"] = $"{resolveIndex}";
            cfg.Save();
        }
    }
}
