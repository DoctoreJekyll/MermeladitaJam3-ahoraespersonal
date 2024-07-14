using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{

    [SerializeField] private UIPaper uiPaper;
    [SerializeField] private String levelName;
    [SerializeField] private Animator animator;

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiPaper.GetTotalPapersPlayerHas() >= uiPaper.GetTotalPapersLevelsNeed())
            {
                StartCoroutine(LoadLevel());
            }
        }
    }

    private IEnumerator LoadLevel()
    {
        animator.Play("fadein");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
}
