using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]
public class LockCinemachineAxis : CinemachineExtension
{
    [Tooltip("Lock the Cinamachine Virtuýal Camera' s X Axis position with this specific value")]
    public float XClampValue = 0;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = XClampValue;
            state.RawPosition = pos;
        }
    }
}
