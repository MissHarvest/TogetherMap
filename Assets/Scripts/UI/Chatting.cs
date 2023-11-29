using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System;
using UnityEngine.EventSystems;

public class Chatting : MonoBehaviour
{
    InputField _inputField;
    [SerializeField] TextMeshProUGUI ChatLog;
    public List<string> _chat = new List<string>();

    private void Awake()
    {
        _inputField = transform.Find("InputField").GetComponent<InputField>();        
        ChatLog.text = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputField.onSubmit.AddListener(SendChat);
        GameManager.Instance.OnChat += ReceiveMsg;
        Debug.Log("Chat Start");
        GameManager.Instance.OnSetPlayer += BindingPlayer;
    }

    void SendChat(string msg)
    {
        if (msg != "") GameManager.Instance.Client.SendMsg($"chat:{PlayerPrefs.GetString("Name")}, {msg}\n");
        _inputField.text = "";
        EventSystem.current.SetSelectedGameObject(null);
    }

    void ReceiveMsg(string msg)
    {
        _chat.Add(msg);
        if(_chat.Count > 5)
        {
            _chat.RemoveAt(0);
        }

        StringBuilder sb = new StringBuilder();

        for(int i = 0; i < _chat.Count; ++i)
        {
            sb.Append(_chat[i]);
            sb.Append("\n");
        }

        ChatLog.text = sb.ToString();
    }

    void BindingPlayer()
    {
        GameManager.Instance.Player.gameObject.GetComponent<PlayerInputController>().OnChatActiveEvent += ActivateChat;
    }

    void ActivateChat()
    {
        Debug.Log($"Press Enter {_inputField.isFocused} {this.gameObject.name}");
        if (_inputField.isFocused == false)
            _inputField.Select();
    }
}
