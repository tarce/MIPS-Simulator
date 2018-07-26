using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryLogItem : MonoBehaviour {

    private Text _text;

    private void OnEnable()
    {
        _text = GetComponent<Text>();
    }

    public void SetText(string binary)
    {
        _text.text = binary;
    }
}
