using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals : MonoBehaviour
{
    #region Singleton
    public static CoreGameSignals Instance;
    private void Awake()
    {
        if(Instance != null && Instance!= this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion
    public UnityAction<byte> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onLevelSuccessfull = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onReset = delegate { };
    public Func<byte> onGetLevelValue = delegate { return 0; };
    public Func<byte> onGetLevelTextValue = delegate { return 0; };
    public UnityAction onStageAreaEntered = delegate { };
    public UnityAction<GameObject[]> onCloseGameObjects = delegate { };
    public UnityAction<byte> onStageAreaSuccessFull = delegate { };
    public UnityAction onFinishAreaEntered = delegate { };
    public UnityAction<byte> onIncreaseCollectedCount = delegate { };
}
