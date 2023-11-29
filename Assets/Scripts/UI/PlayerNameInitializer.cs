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
        string idCheckor = Regex.Replace(nameInput, @"[^0-9a-zA-Z��-�R]", "", RegexOptions.Singleline);
        if(nameInput.Equals(idCheckor) == false)
        {
            warningText.text = "Ư������, ������ ������ �ʽ��ϴ�.";
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
