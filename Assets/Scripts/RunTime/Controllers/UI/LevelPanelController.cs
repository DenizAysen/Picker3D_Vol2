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
        UISignals.Instance.onSetFillValue += OnSetFillValue;
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

    private void OnSetLevelValues(byte levelValue)
    {
        var additionalValue = ++levelValue;
        levelTexts[0].text = additionalValue.ToString();
        additionalValue++;
        levelTexts[1].text = additionalValue.ToString();
    }
    private void OnSetFillValue(float collectedValue)
    {
        slider.DOValue(collectedValue, .75f).OnComplete(() => ChangePercentageText(slider.value*100));
       // Debug.Log("Slider "+slider.normalizedValue);
        //percentageText.text = "" + (slider.value * 100f);
        //slider.value = collectedValue;
        fillImage.color = gradient.Evaluate(slider.value);
    }
    private void ChangePercentageText(float value)
    {
        percentageText.text = percentage + value.ToString("0");
    }
    private void UnSubscribeEvents()
    {
        UISignals.Instance.onSetLevelValue -= OnSetLevelValues;
        UISignals.Instance.onStageColor -= OnSetStageColor;
        UISignals.Instance.onSetFillValue -= OnSetFillValue;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    //IEnumerator ChangeColor(Image image)
    //{
    //    int counter = 0;
    //    while(counter <100 )
    //    yield return new WaitForSeconds(.5f);
    //}
    //private void ChangeStageColor(Image image)
    //{
    //    image.color = Color.Lerp(image.color, targetStageColor, .5f);
    //}
}
