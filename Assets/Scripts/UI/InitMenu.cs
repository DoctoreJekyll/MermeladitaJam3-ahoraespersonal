using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitMenu : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Tuto");
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    
    
}
