using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text timer;

    public void UpdateTimer(float time) {
        timer.text = time.ToString("0.00");
    }
}
