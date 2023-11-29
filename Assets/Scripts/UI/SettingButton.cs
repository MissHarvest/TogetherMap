using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    Button _selfBtn;    
    [SerializeField]GameObject _detail;
    [SerializeField] Button nameChangeBtn;
    [SerializeField] Button characterChangeBtn;

    private void Awake()
    {
        _detail = transform.Find("Detail").gameObject;
        _selfBtn = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _selfBtn.onClick.AddListener(() =>
        {
            _detail.SetActive(!_detail.activeSelf);
        });

        nameChangeBtn.onClick.AddListener(() => 
        {
            Instantiate(Resources.Load<GameObject>("UI/PlayerNameConverter"));
        });

        characterChangeBtn.onClick.AddListener(() => 
        {
            Instantiate(Resources.Load<GameObject>("UI/PlayerCharacterConverter"));
        });
    }


}
