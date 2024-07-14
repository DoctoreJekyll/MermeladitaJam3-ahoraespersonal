using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{

    [SerializeField] private Animator animator;

    private void Start()
    {
        StartCoroutine(InitCorroutine());
    }

    IEnumerator InitCorroutine()
    {
        yield return new WaitForSeconds(2.5f);
        animator.Play("fadein");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Menu");
    }
}
