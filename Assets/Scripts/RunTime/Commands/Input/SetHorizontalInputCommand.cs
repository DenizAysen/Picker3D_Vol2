using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHorizontalInputCommand 
{
    private InputManager _manager;
    private InputData _inputData;
    private Vector3 _moveVector;
    private float _currentVelocity;
    public SetHorizontalInputCommand(InputManager manager , InputData data)
    {
        _manager = manager;
        _inputData = data;
    }
    internal void Execute(Vector2 mouseDeltaPos)
    {
        if (mouseDeltaPos.x > _inputData.HorizontalInputSpeed)
        {
            _moveVector.x = _inputData.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
        }
        else if (mouseDeltaPos.x < _inputData.HorizontalInputSpeed)
        {
            _moveVector.x = -_inputData.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
        }
        else
        {
            _moveVector.x = Mathf.SmoothDamp(-_moveVector.x, 0f, ref _currentVelocity, _inputData.ClampSpeed);
        }
        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
        {
            HorziontalValue = _moveVector.x,
            ClampValues = _inputData.ClampValues
        });
    }
}
