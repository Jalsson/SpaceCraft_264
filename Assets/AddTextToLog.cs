using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddTextToLog : MonoBehaviour {

    #region instance and Wake func
    public static AddTextToLog instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public TextMeshProUGUI eventlog;

    public void AddTextToEventLog(string message)
    {
        eventlog.text += "-" + message + '\n' +"-------------------------------------------------------------------------" +'\n';
    }

}
