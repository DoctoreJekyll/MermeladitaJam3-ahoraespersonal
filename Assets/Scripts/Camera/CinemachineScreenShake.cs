using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineScreenShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulseSource;

    // Call this method to trigger the screen shake
    public void TriggerShake()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
