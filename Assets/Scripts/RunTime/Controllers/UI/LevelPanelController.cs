using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelPanelController : MonoBehaviour
{
    #region Self Variables

    #region Serialiazed Variables

    [SerializeField] private List<Image> stageImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> levelTexts = new();
    [SerializeField] private Color targetStageColor;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Percentage Bar")]
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI percentageText;
    #endregion

    #region Private Variables

    private string percentage = "%";

    #endregion

    #endregion
    private void OnEnable()
    {
        SubscribeEvents();
    }
   
    private void SubscribeEvents()
    {
        UISignals.Instance.onSetLevelValue += OnSetLevelValues;
        UISignals.Instance.onStageColor += OnSetStageColor;
        UISignals.Instance.onIncreaseFillValue += OnIncreaseFillValue;
        UISignals.Instance.onDecreaseFillValue += OnDecreaseFillValue;
        UISignals.Instance.onResetStageColors += OnResetStageColors;
        UISignals.Instance.onSetScoretext += OnSetScoreText;
        MiniGameSignals.Instance.onGetReward += OnGetRewardText;
    }
    [NaughtyAttributes.Button]
    private void OnSetStageColor0()
    {
        stageImages[0].DOColor(targetStageColor, 0.5f);
    }
    [NaughtyAttributes.Button]
    private void OnSetStageColor1()
    {
        stageImages[1].DOColor(targetStageColor, 0.5f);
    }
    [NaughtyAttributes.Button]
    private void OnSetStageColor2()
    {
        stageImages[2].DOColor(targetStageColor, 0.5f);
    }

    private void OnSetStageColor(byte stageValue)
    {
        stageImages[stageValue].DOColor(targetStageColor, 0.5f);
    }
    private void OnResetStageColors()
    {
        for (int i = 0; i < stageImages.Count; i++)
        {
            stageImages[i].color = Color.white;
        }
    }
    private void OnSetLevelValues(byte levelValue)
    {
        var additionalValue = ++levelValue;
        levelTexts[0].text = additionalValue.ToString();
        additionalValue++;
        levelTexts[1].text = additionalValue.ToString();
    }
    private void OnIncreaseFillValue(float collectedValue)
    {
        slider.DOValue(collectedValue, .75f).OnComplete(() => ChangePercentageText(slider.value*100));
        fillImage.color = gradient.Evaluate(slider.value);
    }
    private void OnDecreaseFillValue(float collectedValue)
    {
        slider.DOValue(collectedValue, 1f).OnComplete(() => ChangePercentageText(slider.value * 100));
        fillImage.color = gradient.Evaluate(slider.value);
    }
    private void OnGetRewardText(short rewardValue)
    {
        SaveSignals.Instance.onSaveMoney?.Invoke((short)(SaveSignals.Instance.onGetEarnedMoney?.Invoke() + rewardValue));
        scoreText.text = (SaveSignals.Instance.onGetEarnedMoney?.Invoke()).ToString();
    }
    private void OnSetScoreText()
    {
        scoreText.text = (SaveSignals.Instance.onGetEarnedMoney?.Invoke()).ToString();
    }
    private void ChangePercentageText(float value)
    {
        percentageText.text = percentage + value.ToString("0");
    }
    private void UnSubscribeEvents()
    {
        UISignals.Instance.onSetLevelValue -= OnSetLevelValues;
        UISignals.Instance.onStageColor -= OnSetStageColor;
        UISignals.Instance.onIncreaseFillValue -= OnIncreaseFillValue;
        UISignals.Instance.onResetStageColors -= OnResetStageColors;
        UISignals.Instance.onDecreaseFillValue -= OnDecreaseFillValue;
        UISignals.Instance.onSetScoretext -= OnSetScoreText;
        MiniGameSignals.Instance.onGetReward -= OnGetRewardText;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

}
