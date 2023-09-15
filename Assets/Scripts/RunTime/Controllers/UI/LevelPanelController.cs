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
    private void UnSubscribeEvents()
    {
        UISignals.Instance.onSetLevelValue -= OnSetLevelValues;
        UISignals.Instance.onStageColor -= OnSetStageColor;
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
