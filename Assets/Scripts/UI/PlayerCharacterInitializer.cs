using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacterInitializer : MonoBehaviour
{
    [SerializeField] Text warningText;

    public void SelectCharactor(int index)
    {
        PlayerPrefs.SetInt("Character", index);
    }

    public void CallEnterGameEvent()
    {
        if(PlayerPrefs.HasKey("Character"))
        {
            GameManager.Instance.EnterGame(); 
        }
        else
        {
            warningText.text = "ĳ���͸� �������ּ���.";
        }
    }
}
