using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadManager : MonoBehaviour
{

    public static DeadManager Instance;

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
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
