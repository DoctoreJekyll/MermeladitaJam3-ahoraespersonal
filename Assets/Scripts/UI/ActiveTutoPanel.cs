using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTutoPanel : MonoBehaviour
{

    [SerializeField] private GameObject txtPanel;

    private void Start()
    {
        StartCoroutine(WaitToActivePanel());
    }

    IEnumerator WaitToActivePanel()
    {
        yield return new WaitForSeconds(1.5f);
        txtPanel.SetActive(true);
    }
}
