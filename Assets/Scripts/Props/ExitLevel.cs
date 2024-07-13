using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{

    [SerializeField] private UIPaper uiPaper;

    private void Update()
    {
        if (uiPaper.GetTotalPapers() >= 3)
        {
            Debug.Log("exit");
        }
    }
}
