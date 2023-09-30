using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private new Renderer renderer;
    [SerializeField] private TextMeshPro scaleText;
    [SerializeField] private ParticleSystem confettiParticle;

    #endregion

    #region Private Variables

    private PlayerMeshData _data;
    private float _defaultPlayerXScale;
    private float _currentPlayerXScale;
    #endregion

    #endregion

    private void Awake()
    {
        _defaultPlayerXScale = renderer.gameObject.transform.localScale.x;
    }

    internal void SetData(PlayerMeshData scaleData)
    {
        _data = scaleData;
    }
    internal void ScaleUpPlayer()
    {
        _currentPlayerXScale = renderer.gameObject.transform.localScale.x;
        renderer.gameObject.transform.DOScaleX(_currentPlayerXScale * _data.ScaleCounter, 1).SetEase(Ease.Flash);
    }
    //internal void ScaleUpText()
    //{
    //    renderer.gameObject.transform.DOScaleX(_data.ScaleCounter, 1f).SetEase(Ease.Flash);
    //}
    internal void ShowUpText()
    {
        scaleText.gameObject.SetActive(true);
        scaleText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() => scaleText.DOFade(0, 0).SetDelay(.65f));
        scaleText.rectTransform.DOAnchorPosY(2f,.65f).SetEase(Ease.Linear).OnComplete(() =>
         scaleText.rectTransform.DOAnchorPosY(1.25f, .65f));

    }
    internal void PlayConfettiParticle()
    {
        confettiParticle.Play();        
    }
    internal void OnReset()
    {
        renderer.gameObject.transform.DOScaleX(_defaultPlayerXScale, 1).SetEase(Ease.Linear);
    }
}
