using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameSignals : MonoBehaviour
{
    #region Singleton
    public static MiniGameSignals Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion
    public UnityAction onMiniGameAreaEntered = delegate { };
    public UnityAction<float> onSetRewardAreaPosition= delegate { };
    public Func<float> onGetCollectedPercentageValue = delegate { return 0; };
    public UnityAction<short> onGetReward = delegate { };
    public UnityAction<Vector3> onMoveToNextLevel = delegate { };
    public UnityAction onMiniGameAreaSuccessFull = delegate { };
}
