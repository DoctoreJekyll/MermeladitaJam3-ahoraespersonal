using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadManager : MonoBehaviour
{

    public static DeadManager Instance;

    [SerializeField] private GameObject dead;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        if (!Mathf.Approximately(Time.timeScale, 1))
        {
            Time.timeScale = 1;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ODeath()
    {
        StartCoroutine(RechargeLevel());
    }
    
    private IEnumerator RechargeLevel()
    {
        animator.Play("fadein");
        GameObject player = FindObjectOfType<Move>().gameObject;
        Destroy(player);
        Instantiate(dead, player.transform.position, quaternion.identity);
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
