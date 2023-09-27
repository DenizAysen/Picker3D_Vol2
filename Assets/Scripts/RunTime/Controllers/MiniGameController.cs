using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Transform startPos, endPos, finishPos;
    [SerializeField] private List<RewardController> rewardControllers = new();
    #endregion

    #endregion
    private void Start()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        MiniGameSignals.Instance.onMiniGameAreaEntered += OnMiniGameAreaEntered;
        MiniGameSignals.Instance.onGetReward += OnGetReward;
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
        MiniGameSignals.Instance.onSetRewardAreaPosition?.Invoke(targetZPos);
    }
    private void OnGetReward(short winningRewardValue)
    {
        foreach (var reward in rewardControllers)
        {
            if (reward.GetRewardValue() == winningRewardValue) continue;
            reward.gameObject.SetActive(false);
        }
        StartCoroutine(MoveToNextLevel());
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        MiniGameSignals.Instance.onMiniGameAreaEntered -= OnMiniGameAreaEntered;
        MiniGameSignals.Instance.onGetReward -= OnGetReward;
    }
    private IEnumerator MoveToNextLevel()
    {
        yield return new WaitForSeconds(1f);
        MiniGameSignals.Instance.onMoveToNextLevel?.Invoke(finishPos.position);
    }
}
