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
    [SerializeField] private GameObject[] collectibles;

    #endregion

    #region Private Variables

    private PoolData _data;
    private byte _collectedCount;
    private const string _collectable = "Collectable";
    private Color _stageColor;
    #endregion

    #endregion
    private void Awake()
    {
        _data = GetPoolData();
        _stageColor = GetStageColor();
    }

    private PoolData GetPoolData()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
    }
    private Color GetStageColor()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].StageColor;
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
        renderer.material.DOColor(_stageColor, .75f).SetEase(Ease.Linear);
        //ew Color(0.16f, 0.6f, 0.176f)
    }

    private void OnActivateAnimations(byte stageValue)
    {
        if (stageValue != stageID) return;
        foreach (var tween in tweens)
        {
            tween.DOPlay();
        }
        CoreGameSignals.Instance.onCloseGameObjects?.Invoke(collectibles);
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
    public byte GetCollectedCount()
    {
        return _collectedCount;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_collectable)) return;
        //DecreaseCollectedAmount();
        ManageCollectedAmount(-1);
        SetCollectedAmountToPool();
    }
    
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onStageAreaSuccessFull -= OnActivateAnimations;
        CoreGameSignals.Instance.onStageAreaSuccessFull -= OnChangePoolColor;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
