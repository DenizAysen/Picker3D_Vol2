using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    #region Self Variables

    #region Seriliazed Variables

    [SerializeField] private List<Transform> layers = new();
    [SerializeField] private GameObject uiRoot;
    #endregion

    #endregion

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if(CoreUISignals.Instance == null)
        {
            Debug.Log("CoreUISignals olusmadi");
            CoreUISignals.Instance = uiRoot.AddComponent<CoreUISignals>();
        }
        
        CoreUISignals.Instance.onClosePanel += OnClosePanel;
        CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
        CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
    }

    private void OnCloseAllPanels()
    {
        foreach (var layer in layers)
        {
            if (layer.childCount <= 0) continue;
#if UNITY_EDITOR
            DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
        }
        Debug.Log("Bu metod cagirildi");
    }
    [NaughtyAttributes.Button]
    private void OpenLevelPanel()
    {
        Instantiate(Resources.Load<GameObject>("Screens/LevelPanel"), layers[0]);
    }
    private void OnOpenPanel(UIPanelTypes panelType, int value)
    {
        OnClosePanel(value);
        Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"),layers[value]);
    }

    private void OnClosePanel(int value)
    {
        if (layers[value].childCount <= 0) return;
        
#if UNITY_EDITOR
            DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
            Destroy(layers[value].GetChild(0).gameObject);
#endif
        
    }
    private void UnSubscribeEvents()
    {
        CoreUISignals.Instance.onClosePanel -= OnClosePanel;
        CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
        CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
