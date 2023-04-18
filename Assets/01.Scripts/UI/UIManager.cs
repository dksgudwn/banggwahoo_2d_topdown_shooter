using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI _tmpCurrentAmmo;
    private TextMeshProUGUI _tmpMaxAmmo;

    private void Awake()
    {
        _tmpCurrentAmmo = transform.Find("txtCurrent").GetComponent<TextMeshProUGUI>();
        _tmpMaxAmmo =transform.Find("TxtMax").GetComponent<TextMeshProUGUI>();
    }

    public void SetMaxAmmo(int current, int max)
    {
        _tmpCurrentAmmo.SetText(current.ToString());
        _tmpMaxAmmo.SetText(max.ToString());
    }

    public void SetCurrentAmmo(int current)
    {
        _tmpCurrentAmmo.SetText(current.ToString());
    }
}
