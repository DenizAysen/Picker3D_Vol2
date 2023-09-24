using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Transform startPos, endPos;

    #endregion

    #endregion
    private void Start()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        MiniGameSignals.Instance.onMiniGameAreaEntered += OnMiniGameAreaEntered;
    }

    private void OnMiniGameAreaEntered()
    {
        TargetZPos(MiniGameSignals.Instance.onGetCollectedPercentageValue?.Invoke());
    }

    private void TargetZPos(float? percentage)
    {
        Debug.Log("Donusmeden onceki yuzdelik : " + percentage);
        int _percentage = Convert.ToInt32(percentage);
        Debug.Log("Yuzdelik : " + _percentage);
        float distance = endPos.position.z - startPos.position.z;
        float targetZPos = ((distance * _percentage) / 100f) + startPos.position.z;
        MiniGameSignals.Instance.onsetRewardAreaPosition?.Invoke(targetZPos);
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        MiniGameSignals.Instance.onMiniGameAreaEntered -= OnMiniGameAreaEntered;
    }
}
