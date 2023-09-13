using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnReset()
    {
        CoreUISignals.Instance.onCloseAllPanels?.Invoke();
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
    }

    private void OnLevelFailed()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
    }

    private void OnLevelSuccessfull()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
    }

    private void OnLevelInitialize(byte arg0)
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 0);
        UISignals.Instance.onSetLevelValue?.Invoke((byte)(CoreGameSignals.Instance.onGetLevelValue?.Invoke()));
    }
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onReset -= OnReset;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    public void NextLevel()
    {
        CoreGameSignals.Instance.onNextLevel?.Invoke();
    }
    public void RestartLevel()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
    }
    public void Play()
    {
        UISignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(1);
        InputSignals.Instance.onEnableInput?.Invoke();
    }
}
