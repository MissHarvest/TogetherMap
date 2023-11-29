using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterConverter : MonoBehaviour
{
    static PlayerCharacterConverter _self;

    [SerializeField] Text warningText;

    private void Awake()
    {
        if (_self == null)
        {
            _self = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectCharactor(int index)
    {
        PlayerPrefs.SetInt("Character", index);
    }

    public void ChangePlayerCharacter()
    {
        string id = GameManager.Instance.UID;
        GameManager.Instance.Client.SendMsg($"change:{id},{PlayerPrefs.GetString("Name")},{PlayerPrefs.GetInt("Character")}");
        Destroy(gameObject);
    }
}
