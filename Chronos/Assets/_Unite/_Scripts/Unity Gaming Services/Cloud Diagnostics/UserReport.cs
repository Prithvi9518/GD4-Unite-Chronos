using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserReport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.CrashReportHandler.CrashReportHandler.SetUserMetadata("key", "value");
        Debug.LogException(new Exception("Testing Cloud Diagnostics reports"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
