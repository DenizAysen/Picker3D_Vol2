using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private TextMeshPro rewardText;
    [SerializeField] private short value;

    #endregion

    #endregion
    private void Awake()
    {
        rewardText.text = value.ToString();
    }
    public short GetRewardValue()
    {
        return value;
    }
}
