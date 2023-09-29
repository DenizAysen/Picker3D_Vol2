using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void SubscribeEvents()
    {
        SaveSignals.Instance.onGetGameLoopCount += GetGameLoopCount;
        SaveSignals.Instance.onSaveGameLoopCount += SaveGameLoopCount;
        SaveSignals.Instance.onGetEarnedMoney += GetMoney;
        SaveSignals.Instance.onSaveMoney += SaveMoney;
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SaveGameLoopCount(short value)
    {
        PlayerPrefs.SetInt("GameLoop", Convert.ToInt32(value));
    }
    private void SaveMoney(short value)
    {
        PlayerPrefs.SetInt("Money", Convert.ToInt32(value));
    }
    private byte GetGameLoopCount()
    {
        return (byte) PlayerPrefs.GetInt("GameLoop");
    }
    private short GetMoney()
    {
        return (short) PlayerPrefs.GetInt("Money");
    }

    private void UnSubscribeEvents()
    {
        SaveSignals.Instance.onGetGameLoopCount -= GetGameLoopCount;
        SaveSignals.Instance.onSaveGameLoopCount -= SaveGameLoopCount;
        SaveSignals.Instance.onGetEarnedMoney -= GetMoney;
        SaveSignals.Instance.onSaveMoney -= SaveMoney;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
