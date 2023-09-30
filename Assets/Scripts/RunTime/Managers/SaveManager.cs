using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void SubscribeEvents()
    {
        if (SaveSignals.Instance == null) Debug.Log("Save Signals olusmadi");
        SaveSignals.Instance.onGetGameLoopCount += OnGetGameLoopCount;
        SaveSignals.Instance.onSaveGameLoopCount += OnSaveGameLoopCount;
        SaveSignals.Instance.onGetEarnedMoney += OnGetMoney;
        SaveSignals.Instance.onSaveMoney += OnSaveMoney;
        SaveSignals.Instance.onSaveLastPlayedLevel += OnSaveLastPlayedLevel;
        SaveSignals.Instance.onGetLastPlayedLevelIndex += OnGetLastPlayedLevelIndex;
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void OnSaveGameLoopCount(short value)
    {
        PlayerPrefs.SetInt("GameLoop", Convert.ToInt32(value));
    }
    private void OnSaveMoney(short value)
    {
        PlayerPrefs.SetInt("Money", Convert.ToInt32(value));
    }
    private void OnSaveLastPlayedLevel(short value)
    {
        PlayerPrefs.SetInt("Level", Convert.ToInt32(value));
    }
    private byte OnGetGameLoopCount()
    {
        return (byte) PlayerPrefs.GetInt("GameLoop");
    }
    private byte OnGetLastPlayedLevelIndex()
    {
        return (byte)PlayerPrefs.GetInt("Level");
    }
    private short OnGetMoney()
    {
        return (short) PlayerPrefs.GetInt("Money");
    }

    private void UnSubscribeEvents()
    {
        SaveSignals.Instance.onGetGameLoopCount -= OnGetGameLoopCount;
        SaveSignals.Instance.onSaveGameLoopCount -= OnSaveGameLoopCount;
        SaveSignals.Instance.onGetEarnedMoney -= OnGetMoney;
        SaveSignals.Instance.onSaveMoney -= OnSaveMoney;
        SaveSignals.Instance.onSaveLastPlayedLevel -= OnSaveLastPlayedLevel;
        SaveSignals.Instance.onGetLastPlayedLevelIndex -= OnGetLastPlayedLevelIndex;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
