using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TestCinemachineConfine : CinemachineExtension
{
    [Tooltip("Lock the camera's X position to this value")]
    public float m_XPosition = 0;
    [Tooltip("Lock the camera's Z position to this value")]
    public float m_ZPosition = -10;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = m_XPosition; // Lock the X position
            pos.z = m_ZPosition; // Lock the Z position
            state.RawPosition = pos;
        }
    }
}
