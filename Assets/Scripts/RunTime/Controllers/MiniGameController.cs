using DG.Tweening;
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
    [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();

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
        MiniGameSignals.Instance.onMiniGameAreaSuccessFull += OnActivateAnimations;
    }

    private void OnMiniGameAreaEntered()
    {
        //UISignals.Instance.onSetLevelValue?.Invoke((byte)(CoreGameSignals.Instance.onGetLevelValue?.Invoke() + 1));
        UISignals.Instance.onSetLevelValue?.Invoke((byte)(CoreGameSignals.Instance.onGetLevelTextValue?.Invoke() + 1));
        UISignals.Instance.onResetStageColors?.Invoke();
        TargetZPos(MiniGameSignals.Instance.onGetCollectedPercentageValue?.Invoke());
    }

    private void TargetZPos(float? percentage)
    {     
        int _percentage = Convert.ToInt32(percentage);
        float distance = endPos.position.z - startPos.position.z;
        float targetZPos = ((distance * _percentage) / 100f) + startPos.position.z;
        MiniGameSignals.Instance.onSetRewardAreaPosition?.Invoke(targetZPos);
        UISignals.Instance.onDecreaseFillValue?.Invoke(0);
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
    private IEnumerator MoveToNextLevel()
    {
        yield return new WaitForSeconds(1f);
        MiniGameSignals.Instance.onMoveToNextLevel?.Invoke(finishPos.position);
        MiniGameSignals.Instance.onMiniGameAreaSuccessFull?.Invoke();
    }
    private void OnActivateAnimations()
    {
        foreach (var tween in tweens)
        {
            tween.DOPlay();
        }
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        MiniGameSignals.Instance.onMiniGameAreaEntered -= OnMiniGameAreaEntered;
        MiniGameSignals.Instance.onGetReward -= OnGetReward;
        MiniGameSignals.Instance.onMiniGameAreaSuccessFull -= OnActivateAnimations;
    } 
}
