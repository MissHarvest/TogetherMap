using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Client
{
    Socket _clientSocket;
    public string serverIP = "220.81.47.206";//"192.168.0.16";
    static int port = 12345;
    public bool connected = false;
    public bool haveMsg = false;
    string msg;
    public string MSG { get { haveMsg = false; return msg; } }

    public void Init()
    {
        _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //serverIP = GetIPAddress().ToString();
    }

    public static IPAddress GetIPAddress()
    {
        IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = null;

        foreach(var addr in he.AddressList)
        {
            if(addr.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddress = addr;
                break;
            }
        }

        if (ipAddress == null)
            ipAddress = IPAddress.Loopback;

        return ipAddress;
    }

    public bool ConnectToServer()
    {
        IPAddress ip = IPAddress.Parse(serverIP);
        IPEndPoint ep = new IPEndPoint(ip, port);
        try
        {
            _clientSocket.Connect(ep);
            //_clientSocket.Close();
            connected = true;
            Debug.Log("Client Connect to Sever[" + ep.ToString() + "]");
            return true;
        }
        catch (Exception ex)
        {
            Debug.Log("Connect Exception : " + ex.ToString());
            return false;
        }
    }


    public void Receive_Callback()// in Client.cs
    {
        NetworkStream stream = new NetworkStream(_clientSocket);
        byte[] buffer = new byte[1024];
        try
        {
            while (connected)
            {
                if (haveMsg == false)
                {
                    int bufferSize = stream.Read(buffer, 0, buffer.Length);                    
                    msg = Encoding.UTF8.GetString(buffer, 0, bufferSize);                    
                    Array.Clear(buffer, 0, buffer.Length);
                    haveMsg = true;
                    Debug.Log("Read Msg by Sever : " + msg);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Recevie Callback Exception " + ex.ToString());
        }
        stream.Close();
    }
    public void SendMsg(string msg)//string msg --> Encoding
    {
        byte[] sendMsg = Encoding.UTF8.GetBytes(msg);
        byte[] bytes = new byte[1024];
        for (int i = 0; i < sendMsg.Length; i++) bytes[i] = sendMsg[i];

        _clientSocket.Send(bytes);
    }

    public void ProcssMsg() // in Client.cs
    {
        string reciveMsg = this.MSG;
        string[] msgPart = msg.Split(':');

        string header = msgPart[0];
        string body = msgPart[1];
        string[] bodyPart = body.Split(',');
        string id, name, character;
        Vector2 dir = new Vector2();
        switch (header)
        {
            case "client":
                id = bodyPart[0];
                GameManager.Instance.UID = id;
                break;

            case "enter":
                id = bodyPart[0];
                name = bodyPart[1];
                character = bodyPart[2];
                GameManager.Instance.CharacterSpawner.SpawnCharacter(id, name, int.Parse(character));

                if(id != GameManager.Instance.UID)
                {
                    GameManager.Instance.Client.SendMsg($"add:{id},{GameManager.Instance.UID},{PlayerPrefs.GetString("Name")},{PlayerPrefs.GetInt("Character")}," +
                    $"{GameManager.Instance.Player.gameObject.transform.position.x}, {GameManager.Instance.Player.gameObject.transform.position.y}");
                }
                
                break;

            case "add":
                string target = bodyPart[0];
                if(target == GameManager.Instance.UID)
                {
                    id = bodyPart[1];
                    name = bodyPart[2];
                    character = bodyPart[3];
                    dir.x = float.Parse(bodyPart[4]);
                    dir.y = float.Parse(bodyPart[5]);
                    GameManager.Instance.CharacterSpawner.SpawnCharacter(id, name, int.Parse(character), dir);
                }
                break;

            case "change":
                id = bodyPart[0];
                name = bodyPart[1];
                character = bodyPart[2];
                GameManager.Instance.ChangeUserInfo(id, name, int.Parse(character));
                break;

            case "move": // ip, x, y
                id = bodyPart[0];
                dir.x = float.Parse(bodyPart[1]);
                dir.y = float.Parse(bodyPart[2]);
                GameManager.Instance.MoveCharacter(id, dir);
                break;

            case "look":
                id = bodyPart[0];
                bool bLeft = bool.Parse(bodyPart[1]);
                GameManager.Instance.FlipCharacter(id, bLeft);
                break;

            case "chat":
                string[] t = bodyPart[1].Split('\n');                
                string msg = string.Format("{0} : {1}", bodyPart[0], t[0]);
                GameManager.Instance.ReceiveChat(msg);
                break;

            case "exit":
                id = bodyPart[0];
                GameManager.Instance.RemoveCharacter(id);
                break;
        }
    }

    public void Disconnect()
    {
        SendMsg($"exit:{GameManager.Instance.UID},");
        //_clientSocket.Disconnect(false);
    }

    public void CloseSocket()
    {
        _clientSocket.Close();
    }
}
