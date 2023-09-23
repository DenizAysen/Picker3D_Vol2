using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Rigidbody rigidbody;
    // [SerializeField] private new Collider collider;

    #endregion

    #region Private Variables

    private PlayerMovementData _data;
    private bool _isReadyToMove, _isReadyToPlay;
    private float _xValue;

    private float2 _clampValues;

    #endregion

    #endregion

    internal void SetData(PlayerMovementData data)
    {
        _data = data;
    }
 
    private void FixedUpdate()
    {
        if (!_isReadyToPlay)
        {
            StopPlayer();
            return;
        }
        if (_isReadyToMove)
        {
            MovePlayer();
        }
        else
        {
            StopPlayerHorizontally();
        }
    }
    private void StopPlayer()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
    private void StopPlayerHorizontally()
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
        rigidbody.angularVelocity = Vector3.zero;
    }
    private void MovePlayer()
    {
        var velocity = rigidbody.velocity;
        velocity = new Vector3(_xValue * _data.SideWaySpeed, velocity.y, _data.ForwardSpeed);
        rigidbody.velocity = velocity;
        var position1 = rigidbody.position;
        Vector3 position;
        position = new Vector3(Mathf.Clamp(position1.x, _clampValues.x, _clampValues.y),
            (position = rigidbody.position).y , position.z);
        rigidbody.position = position;
    }

    internal void IsReadyToPlay(bool condition)
    {
        _isReadyToPlay = condition;
    }
    internal void IsReadyToMove(bool condition)
    {
        _isReadyToMove = condition;
    }

    internal void UpdateInputParams(HorizontalInputParams inputParams)
    {
        _xValue = inputParams.HorziontalValue;
        _clampValues = inputParams.ClampValues;
    }

    internal void OnReset()
    {
        StopPlayer();
        _isReadyToMove = false;
        _isReadyToPlay = false;
    }
}
