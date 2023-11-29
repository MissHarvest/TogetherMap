using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Text nameText;
    Animator _animator;
    public string Name { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        ChangeName(PlayerPrefs.GetString("Name"));
        ChangeCharacter(PlayerPrefs.GetInt("Character"));
    }

    public void ChangeName(string name)
    {
        Name = name;
        nameText.text = name;
    }

    public void ChangeCharacter(int character)
    {
        _animator.runtimeAnimatorController = RuntimeAnimatorController.Instantiate(
            Resources.Load<RuntimeAnimatorController>($"Animator/Character{character}"));
    }
}
