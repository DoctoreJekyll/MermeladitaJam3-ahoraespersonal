using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{

    [SerializeField] private UIPaper uiPaper;
    [SerializeField] private String levelName;

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiPaper.GetTotalPapersPlayerHas() >= uiPaper.GetTotalPapersLevelsNeed())
            {
                LoadLevel();
            }
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
