using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNameSlot : MonoBehaviour
{
    Text _userNameText;

    private void Awake()
    {
        _userNameText = transform.Find("NameText").GetComponent<Text>();
    }

    public void SetText(string name)
    {
        _userNameText.text = name;
    }
}
