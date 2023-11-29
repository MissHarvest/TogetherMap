using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    Text _timeText;

    private void Awake()
    {
        _timeText = gameObject.transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeText.text = DateTime.Now.ToString("HH:mm");
    }
}
