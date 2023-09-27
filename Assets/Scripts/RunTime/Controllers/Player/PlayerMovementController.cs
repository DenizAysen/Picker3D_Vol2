using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;
public class PlayerMovementController : MonoBehaviour
{

    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private PlayerManager manager;
    // [SerializeField] private new Collider collider;

    #endregion

    #region Private Variables

    private PlayerMovementData _data;
    private bool _isReadyToMove, _isReadyToPlay, _inMiniGameArea, _hasRewardGiven;
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
        if (_inMiniGameArea)
        {
            if (_hasRewardGiven) return;
            MovePlayerHorizontally();
        }
        else
        {
            if (_isReadyToMove)
            {
                MovePlayer();
            }
            else
            {
                StopPlayerHorizontally();
            }
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
    private void MovePlayerHorizontally()
    {
        var velocity = rigidbody.velocity;
        velocity = new Vector3(_xValue * _data.SideWaySpeed, velocity.y, 0);
        rigidbody.velocity = velocity;
        var position1 = rigidbody.position;
        Vector3 position;
        position = new Vector3(Mathf.Clamp(position1.x, _clampValues.x, _clampValues.y),
            (position = rigidbody.position).y, position.z);
        rigidbody.position = position;
    }
    internal void MovePlayerToTargetedZPos(float zValue)
    {
        transform.DOMoveZ(zValue, 1f).OnComplete(() => manager.GiveRewardCommmand.Execute());
    }
    internal void MovePlayerToTargetedLocation(Vector3 targetedPos)
    {
        //InMiniGameArea(false);
        _hasRewardGiven = true;
        var _targetedPos = new Vector3(targetedPos.x, transform.position.y, targetedPos.z);
        transform.DOMove(_targetedPos, 3f);
    }
    internal void IsReadyToPlay(bool condition)
    {
        _isReadyToPlay = condition;
    }
    internal void IsReadyToMove(bool condition)
    {
        _isReadyToMove = condition;
    }
    internal void InMiniGameArea(bool condition)
    {
        _inMiniGameArea = condition;
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
