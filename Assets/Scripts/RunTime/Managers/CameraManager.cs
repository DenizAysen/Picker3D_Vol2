using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;
using System;

public class CameraManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    #endregion

    #region Private Variables

    private float3 _firstPos;

    #endregion

    #endregion
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _firstPos = transform.position;
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnReset()
    {
        transform.position = _firstPos;
    }

    private void OnSetCameraTarget()
    {
        //var player = FindObjectOfType<PlayerManager>().transform;
        //virtualCamera.Follow = player;
        //virtualCamera.LookAt = player;
    }
    private void UnSubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
        CoreGameSignals.Instance.onReset -= OnReset;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}