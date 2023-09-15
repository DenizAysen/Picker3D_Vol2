using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public byte StageValue;

    internal ForceBallsToPoolCommand ForceCommand;

    #endregion

    #region Serialized Variables

    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerMeshController meshController;
    [SerializeField] private PlayerPhysicsController physicsController;

    #endregion

    #region Private Variables

    private PlayerData _data;

    #endregion

    #endregion
    private void Awake()
    {
        _data = GetPlayerData();
        SendDataToControllers();
        Init();
    }

    private void Init()
    {
        ForceCommand = new ForceBallsToPoolCommand(this, _data.ForceData);
    }

    private void SendDataToControllers()
    {
        movementController.SetData(_data.MovementData);
        meshController.SetData(_data.MeshData);
    }

    private PlayerData GetPlayerData()
    {
        return Resources.Load<CD_Player>("Data/CD_Player").Data;
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputTaken += OnInputTaken;
        InputSignals.Instance.onInputReleased += OnInputReleased;
        InputSignals.Instance.onInputDragged += OnInputDragged;
        UISignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessFull;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
        CoreGameSignals.Instance.onStageAreaSuccessFull += OnStageAreaSuccessFull;
        CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
        CoreGameSignals.Instance.onReset += OnReset;
    }
    private void OnPlay()
    {
        movementController.IsReadyToPlay(true);
    }

    private void OnInputTaken()
    {
        movementController.IsReadyToMove(true);
    }
    private void OnInputDragged(HorizontalInputParams inputParams)
    {
        movementController.UpdateInputParams(inputParams);
    }

    private void OnInputReleased()
    {
        movementController.IsReadyToMove(false);
    }

    private void OnStageAreaEntered()
    {
        movementController.IsReadyToPlay(false);
    }
    private void OnFinishAreaEntered()
    {
        CoreGameSignals.Instance.onLevelSuccessfull?.Invoke();
        // Mini Game
    }
    private void OnLevelSuccessFull()
    {
        movementController.IsReadyToPlay(false);
    }
    private void OnLevelFailed()
    {
        StageValue = 0;
    }
    private void OnStageAreaSuccessFull(byte value)
    {
        StageValue = (byte)++value;
    }

    private void OnReset()
    {
        StageValue = 0;
        //movementController.OnReset();
        //physicsController.OnReset();
        //meshController.OnReset();
    }
    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputTaken -= OnInputTaken;
        InputSignals.Instance.onInputReleased -= OnInputReleased;
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        UISignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessFull;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
        CoreGameSignals.Instance.onStageAreaSuccessFull -= OnStageAreaSuccessFull;
        CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
        CoreGameSignals.Instance.onReset -= OnReset;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
