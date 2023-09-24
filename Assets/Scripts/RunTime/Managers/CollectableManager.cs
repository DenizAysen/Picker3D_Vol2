using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables

    #region Private Variables

    private short _totalSpawnedCollectableCount;
    private short _collectedCollectableCount;

    #endregion

    #endregion
    private void Start()
    {
        _totalSpawnedCollectableCount = GetTotalSpawnedCollectableCount();
        SubscribeEvents();
    }

    private short GetTotalSpawnedCollectableCount()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)(CoreGameSignals.Instance.onGetLevelValue?.Invoke())].TotalSpawnedCollectableCount;
    }
    private void OnIncreaseCollectedCount(byte collectedCount)
    {
        _collectedCollectableCount += collectedCount;
        float x = _collectedCollectableCount;
        float y = _totalSpawnedCollectableCount;
        //Debug.Log((x / y));
        UISignals.Instance.onSetFillValue?.Invoke(x/y);
    }
    private float ReturnCollectedPercentage()
    {
        float x = _collectedCollectableCount;
        float y = _totalSpawnedCollectableCount;
        float percentage = (x / y) * 100f;
        return percentage;
    }
    private void OnReset()
    {
        _collectedCollectableCount = 0;
    }
    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onIncreaseCollectedCount += OnIncreaseCollectedCount;
        CoreGameSignals.Instance.onReset += OnReset;
        MiniGameSignals.Instance.onGetCollectedPercentageValue += ReturnCollectedPercentage;
    }
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onIncreaseCollectedCount -= OnIncreaseCollectedCount;
        CoreGameSignals.Instance.onReset -= OnReset;
        MiniGameSignals.Instance.onGetCollectedPercentageValue -= ReturnCollectedPercentage;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
