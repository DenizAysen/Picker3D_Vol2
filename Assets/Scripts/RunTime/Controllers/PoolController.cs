using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class PoolController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();
    [SerializeField] private TextMeshPro poolText;
    [SerializeField] private byte stageID;
    [SerializeField] private new Renderer renderer;

    #endregion

    #region Private Variables

    private PoolData _data;
    private byte _collectedCount;
    private const string _collectable = "Collectable";
    #endregion

    #endregion
    private void Awake()
    {
        _data = GetPoolData();
    }

    private PoolData GetPoolData()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onStageAreaSuccessFull += OnActivateAnimations;
        CoreGameSignals.Instance.onStageAreaSuccessFull += OnChangePoolColor;
    }

    private void OnChangePoolColor(byte stageValue)
    {
        if (stageValue != stageID) return;
        renderer.material.DOColor(new Color(0.16f, 0.6f, 0.176f), 1).SetEase(Ease.Linear);

    }

    private void OnActivateAnimations(byte stageValue)
    {
        if (stageValue != stageID) return;
        foreach (var tween in tweens)
        {
            tween.DOPlay();
        }
    }
    
    private void Start()
    {
        SetRequiredAmountText();
    }

    private void SetRequiredAmountText()
    {
        poolText.text = $"0/{_data.RequiredObjectCount}";
    }

    internal bool TakeResults(byte managerStageValue)
    {
        if (stageID == managerStageValue)
        {
            return _collectedCount >= _data.RequiredObjectCount;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_collectable)) return;
        //IncreaseCollectedAmount();
        ManageCollectedAmount(1);
        SetCollectedAmountToPool();
    }

    private void SetCollectedAmountToPool()
    {
        poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";
    }

    private void IncreaseCollectedAmount()
    {
        _collectedCount++;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_collectable)) return;
        //DecreaseCollectedAmount();
        ManageCollectedAmount(-1);
        SetCollectedAmountToPool();
    }
    private void DecreaseCollectedAmount()
    {
        _collectedCount--;
    }
    private void ManageCollectedAmount(short value)
    {
        if (value == 0) return;
        else if (value > 0) _collectedCount++;
        else
        {
            _collectedCount--;
            if (_collectedCount < 0) _collectedCount = 0;
        }

    }
}
