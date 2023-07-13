using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{// ��ư�� ������ ����
    public Button startButton;
    public Button optionButton;
    public Button shopButton;
    private UnityAction action;
    void Start()
    {
        // UnityAction�� ����� �̺�Ʈ ���� ���
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);
        // ���� �޼��带 Ȱ���� �̺�Ʈ ���� ���
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); });
        // ���ٽ��� Ȱ���� �̺�Ʈ ���� ���
        shopButton.onClick.AddListener(() => OnButtonClick(shopButton.name));
    }


    public void OnButtonClick(string msg)
    {
        Debug.Log($"Click Button : {msg}");
    }
    public void OnStartClick()
    {
        SceneManager.LoadScene("Level_01");
        SceneManager.LoadScene("Play", LoadSceneMode.Additive);
    }
}
