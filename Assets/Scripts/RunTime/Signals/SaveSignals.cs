using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveSignals : MonoBehaviour
{
    #region Singleton
    public static SaveSignals Instance;

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
    public UnityAction<short> onSaveGameLoopCount = delegate { };
    public UnityAction<short> onSaveMoney = delegate { };
    public UnityAction<short> onSaveLastPlayedLevel = delegate { };
    public Func<byte> onGetGameLoopCount = delegate { return 0; };
    public Func<byte> onGetLastPlayedLevelIndex = delegate { return 0; };
    public Func<short> onGetEarnedMoney = delegate { return 0; };
}
