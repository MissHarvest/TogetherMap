using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerNameInitializer : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Text warningText;

    // Start is called before the first frame update
    void Start()
    {
        nameInput.onSubmit.AddListener(CheckEnableInput);
    }

    private void CheckEnableInput(string nameInput)
    {
        string idCheckor = Regex.Replace(nameInput, @"[^0-9a-zA-Z°¡-ÆR]", "", RegexOptions.Singleline);
        if(nameInput.Equals(idCheckor) == false)
        {
            warningText.text = "Æ¯¼ö¹®ÀÚ, °ø¹éÀº Çã¿ëµÇÁö ¾Ê½À´Ï´Ù.";
        }
        else
        {
            PlayerPrefs.SetString("Name", nameInput);
            var ui = Resources.Load<GameObject>("UI/PlayerCharacterInitializer");
            Instantiate(ui);
            Destroy(gameObject);
        }
    }

}
