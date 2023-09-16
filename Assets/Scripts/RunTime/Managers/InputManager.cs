using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region Self Variables

    #region Private Variables
    private InputData _data;
    private bool _isAvaibleForTouch, _isFirstTimeTouchTaken,_isTouching;

    private float _currentVelocity;
    private float3 _moveVector;
    private Vector2? _mousePosition;
    #endregion

    #endregion

    private void Awake()
    {
        _data = GetInputData();
    }

    private InputData GetInputData()
    {
        return Resources.Load<CD_Input>("Data/CD_Input").Data;
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onReset += OnReset;
        InputSignals.Instance.onEnableInput += OnEnableInput;
        InputSignals.Instance.onDisableInput += OnDisableInput;
    }

    private void OnReset()
    {
        _isAvaibleForTouch = false;
        //_isFirstTimeTouchTaken = false;
        //FirstTimeTouchTaken kullanýcýdan input alana kadar gosterilmesi gereken durum vardir. Ekrana dokundugunda islem kapatilir. Bu gibi durumlarda kullanilir
        _isTouching = false;
    }
    private void OnEnableInput()
    {
        _isAvaibleForTouch = true;
    }
    private void OnDisableInput()
    {
        _isAvaibleForTouch = false;
    }
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onReset -= OnReset;
        InputSignals.Instance.onEnableInput -= OnEnableInput;
        InputSignals.Instance.onDisableInput -= OnDisableInput;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    private void Update()
    {
        if (!_isAvaibleForTouch) return;

        if(Input.GetMouseButtonUp(0) /*&& !_isPointerOverUIElement()*/)
        {
            Debug.Log("Fareden el cekildi");
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
        }

        if (Input.GetMouseButtonDown(0) /*&& !_isPointerOverUIElement()*/)
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }

            _mousePosition = Input.mousePosition;
            Debug.Log("Ekrana dokunuldu. Dokunulan pozisyon : " + _mousePosition);
        }

        if(Input.GetMouseButton(0) /*&& !_isPointerOverUIElement()*/)
        {
            
            if (_isTouching)
            {
                if(_mousePosition != null)
                {

                    Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;

                    if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                    {
                        _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }
                    else if (mouseDeltaPos.x < _data.HorizontalInputSpeed)
                    {
                        _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                    }
                    else
                    {
                        _moveVector.x = Mathf.SmoothDamp(-_moveVector.x, 0f, ref _currentVelocity, _data.ClampSpeed);
                    }
                    // Debug.Log(_moveVector.x);
                    _moveVector.x = mouseDeltaPos.x;
                    _mousePosition = Input.mousePosition;

                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
                    {
                        HorziontalValue = _moveVector.x,
                        ClampValues = _data.ClampValues
                    });
                }
            }
        }
    }

    //private bool _isPointerOverUIElement()
    //{
    //    var eventData = new PointerEventData(EventSystem.current)
    //    {
    //        position = Input.mousePosition
    //    };
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventData, results);
    //    return results.Count > 0;
    //}
}
