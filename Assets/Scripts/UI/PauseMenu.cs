using System;
using System.Collections;
using System.Collections.Generic;
using Jugador.NewWaterPlayer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private InputActionReference inputs;
    [SerializeField] private PlayerInput playerInput;
    public void ToggleObj()
    {
        bool isActive = !obj.activeSelf;
        obj.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f;

        // Desactivar o activar el mapa de acciones del jugador
        if (playerInput != null)
        {
            if (isActive)
            {
                playerInput.SwitchCurrentActionMap("UI"); // Cambia al mapa de acciones de UI
            }
            else
            {
                playerInput.SwitchCurrentActionMap("Player"); // Cambia al mapa de acciones del jugador
            }
        }

        Debug.Log("Menu toggled: " + isActive);
    }

    public void GoExit()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnEnable()
    {
        inputs.action.performed += MenuAction;
        inputs.action.canceled += MenuAction;
    }

    private void OnDisable()
    {
        inputs.action.performed -= MenuAction;
        inputs.action.canceled -= MenuAction;
    }

    private void MenuAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            ToggleObj();
        }
    }
}

