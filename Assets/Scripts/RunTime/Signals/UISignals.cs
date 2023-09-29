using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoBehaviour
{
    #region Singleton
    public static UISignals Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    public UnityAction<byte> onStageColor = delegate { };
    public UnityAction<byte> onSetLevelValue = delegate { };
    public UnityAction<float> onIncreaseFillValue = delegate { };
    public UnityAction<float> onDecreaseFillValue = delegate { };
    public UnityAction onResetStageColors = delegate { };
    public UnityAction onSetScoretext = delegate { };
    public UnityAction onPlay = delegate { };
}
