using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;

public class PlayerNameConverter : MonoBehaviour
{
    static PlayerNameConverter _self;

    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Text warningText;

    private void Awake()
    {
        if(_self == null)
        {
            _self = this;
            GameManager.Instance.Player.GetComponent<PlayerInput>().enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nameInput.onSubmit.AddListener(CheckEnableInput);
    }

    private void CheckEnableInput(string nameInput)
    {
        string idCheckor = Regex.Replace(nameInput, @"[^0-9a-zA-Z°¡-ÆR]", "", RegexOptions.Singleline);
        if (nameInput.Equals(idCheckor) == false)
        {
            warningText.text = "Æ¯¼ö¹®ÀÚ, °ø¹éÀº Çã¿ëµÇÁö ¾Ê½À´Ï´Ù.";
        }
        else
        {
            PlayerPrefs.SetString("Name", nameInput);
            string id = GameManager.Instance.UID;
            GameManager.Instance.Client.SendMsg($"change:{id},{PlayerPrefs.GetString("Name")},{PlayerPrefs.GetInt("Character")}");
            GameManager.Instance.Player.GetComponent<PlayerInput>().enabled = true;
            Destroy(gameObject);
        }
    }
}
