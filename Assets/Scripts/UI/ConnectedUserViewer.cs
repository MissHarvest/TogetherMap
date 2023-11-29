using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectedUserViewer : MonoBehaviour
{
    Dictionary<string, UserNameSlot> _users = new Dictionary<string, UserNameSlot>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnAddUserEvent += AddUserNameSlot;
        GameManager.Instance.OnChangeUserInfoEvent += ChangeUserName;
        GameManager.Instance.OnExitUserEvent += RemoveUserNameSlot;
    }

    public void AddUserNameSlot(string ip, GameObject go)
    {
        var slot = Instantiate(Resources.Load<GameObject>("UI/UserNameSlot"));
        string name = go.GetComponent<Player>().Name;
        slot.GetComponent<UserNameSlot>().SetText(name);
        slot.transform.parent = this.transform;
        _users.Add(ip, slot.GetComponent<UserNameSlot>());
    }

    public void ChangeUserName(string ip, string name, int character)
    {
        _users[ip].SetText(name);
    }

    public void RemoveUserNameSlot(string id)
    {
        if (_users.ContainsKey(id))
        {
            Destroy(_users[id].gameObject);
            _users.Remove(id);
        }
    }
}
