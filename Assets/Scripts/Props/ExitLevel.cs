using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{

    [SerializeField] private UIPaper uiPaper;

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiPaper.GetTotalPapers() >= 3)
            {
                Debug.Log("exit");
            }
        }
    }
}
