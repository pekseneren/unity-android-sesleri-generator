using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class ButtonManagement : MonoBehaviour {

    public List<string> PATHS = new List<string>();
    public Dictionary<int, Transform> Panels = new Dictionary<int, Transform>();
    public GameObject ButtonPrefab;
    public GameObject PanelPrefab;
    public GameObject ScrollView;
    public TextMeshProUGUI TabText;
    public Transform ViewPort;
    public static AudioSource auidoSource;

    // Use this for initialization
    void Start()
    {
        auidoSource = GameObject.FindWithTag("Source").GetComponent<AudioSource>();

        foreach (var path in PATHS)
        {
            CreateContet(path);
            Debug.Log("kaç kere" + path);
        }

        ViewPort.GetChild(0).gameObject.SetActive(true);
        ChangeContent(ViewPort.GetChild(0));
    }

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

    public void RightChange()
    {
        foreach (Transform content in ViewPort)
        {
            if (content.gameObject.activeSelf == true)
            {
                if(Panels.ContainsKey(content.GetSiblingIndex()+1))
                {
                    ChangeContent(Panels[content.GetSiblingIndex()+1]);
                    content.gameObject.SetActive(false);
                }
                return;
            }
        }
    }

    public void LeftChange()
    {
        foreach (Transform content in ViewPort)
        {
            if (content.gameObject.activeSelf == true)
            {
                if (Panels.ContainsKey(content.GetSiblingIndex() - 1))
                {
                    ChangeContent(Panels[content.GetSiblingIndex() - 1]);
                    content.gameObject.SetActive(false);
                }
                return;
            }
        }
    }
}
