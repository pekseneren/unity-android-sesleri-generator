using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour {

    public static AudioSource auidoSource;

    public List<string> PATHS = new List<string>();
    public Dictionary<int, Transform> Panels = new Dictionary<int, Transform>();
    public GameObject ButtonPrefab;
    public GameObject PanelPrefab;
    public GameObject ScrollView;
    public Transform ViewPort;
    public TextMeshProUGUI TabText;


    void Start()
    {
        PATHS.Clear();
        auidoSource = GameObject.FindGameObjectWithTag("Source").GetComponent<AudioSource>();

        var paths = Directory.GetDirectories(Application.dataPath + "/Resources");
        
        foreach (var path in paths)
        {
            var temp = path.Split('\\');
            Debug.Log("p:");
            CreateContet(temp[temp.Length - 1]);
            PATHS.Add(temp[temp.Length - 1]);
            //Debug.Log("kaç kere" + path);
        }
        ViewPort.GetChild(0).gameObject.SetActive(true);
        ChangeContent(ViewPort.GetChild(0));
    }

    //Creates a tab for folder
    public void CreateContet(string name)
    {
        var clips = Resources.LoadAll<AudioClip>(name);
        var panel = Instantiate(PanelPrefab, ViewPort);
        
        foreach (AudioClip clip in clips)
        {
            string fileName = clip.name;
            var buttonGO = Instantiate(ButtonPrefab, panel.transform);
            buttonGO.GetComponentInChildren<TextMeshProUGUI>().text = fileName.ToUpper();

            buttonGO.GetComponent<DynamicPlay>().auido = clip;
            buttonGO.GetComponent<Button>().onClick.AddListener(buttonGO.GetComponent<DynamicPlay>().PlayAuido);
        }

        Panels.Add(panel.transform.GetSiblingIndex(), panel.transform);
        panel.SetActive(false);
    }

    public void ChangeContent(Transform content)
    {
        TabText.text = PATHS[content.GetSiblingIndex()].ToUpper();
        content.gameObject.SetActive(true);
        ScrollView.GetComponent<ScrollRect>().content = content.GetComponent<RectTransform>();
    }

    public void ChangeTab(int i)
    {
        foreach (Transform content in ViewPort)
        {
            if (content.gameObject.activeSelf == true)
            {
                if (Panels.ContainsKey(content.GetSiblingIndex() + i))
                {
                    ChangeContent(Panels[content.GetSiblingIndex() + i]);
                    content.gameObject.SetActive(false);
                }
                return;
            }
        }
    }

    public void RightChange()
    {
        ChangeTab(1);
    }

    public void LeftChange()
    {
        ChangeTab(-1);
    }
}
