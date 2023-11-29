using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public Client Client;
    //public string ServerIP { get { return Client.serverIP; } }

    public event Action<string, GameObject> OnAddUserEvent;
    public event Action<string, string, int> OnChangeUserInfoEvent;
    public event Action<string> OnChat;

    CharactorSpawner _characterSpawner;
    public CharactorSpawner CharacterSpawner
    {
        get
        {
            if (_characterSpawner == null)
            {
                var go = Instantiate(Resources.Load<GameObject>("CharacterSpawner"));
                _characterSpawner = go.GetComponent<CharactorSpawner>();
            }
            return _characterSpawner;
        }

        set
        {

        }
    }

    string _uid = null;
    public string UID { get { return _uid; } set { if (_uid == null) _uid = value; } }

    public static GameManager Instance { get { return _instance; } }
    Dictionary<string, GameObject> Users = new Dictionary<string, GameObject>();

    Player _player;
    public Player Player 
    {
        get { return _player; }
        set { _player = value; OnSetPlayer?.Invoke(); } 
    }
    public event Action OnSetPlayer;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Client = new Client();
            Client.Init();
            if (Client.ConnectToServer())
            {
                ThreadStart threadstart = new ThreadStart(Client.Receive_Callback);
                Thread thread = new Thread(threadstart);
                thread.Start();
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Client.haveMsg)
        {
            Client.ProcssMsg();
        }
    }

    public void EnterGame()
    {
        SceneManager.LoadScene("Main");
        string msg = $"enter:{UID},{PlayerPrefs.GetString("Name")},{PlayerPrefs.GetInt("Character")}";
        Debug.Log($"Enter Game [{msg}]");
        Client.SendMsg(msg);
    }

    public void AddUser(string ip, GameObject go)
    {
        Users.Add(ip, go);

        foreach(var user in Users)
        {
            Debug.Log($"{user.Key} / {user.Value.name}");
        }

        OnAddUserEvent?.Invoke(ip, go);
    }

    public void ChangeUserInfo(string ip, string name, int character)
    {
        Users[ip].GetComponent<Player>().ChangeName(name);
        Users[ip].GetComponent<Player>().ChangeCharacter(character);
        OnChangeUserInfoEvent?.Invoke(ip, name, character);
    }

    public void MoveCharacter(string ip, Vector2 dir)
    {
        Debug.Log($"{Users.Count}");
        Users[ip].GetComponent<TopDownCharacterController>().CallMoveEvent(dir);
    }

    public void ReceiveChat(string msg)
    {
        OnChat?.Invoke(msg);
    }

    public void FlipCharacter(string id, bool bLeft)
    {
        Users[id].GetComponent<TopDownLookMouseSide>().LookLeft(bLeft);
    }
}
