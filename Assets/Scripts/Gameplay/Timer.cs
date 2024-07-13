using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 180; // Tiempo en segundos
    [SerializeField] private TMP_Text timeText; // Referencia al UI Text para mostrar el tiempo

    private bool timerIsRunning = false;

    private void Start()
    {
        // Iniciar el temporizador
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Tiempo terminado!");
                timeRemaining = 0;
                timerIsRunning = false;
                DeadManager.Instance.ODeath();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Para redondear hacia arriba al siguiente segundo
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
    

}