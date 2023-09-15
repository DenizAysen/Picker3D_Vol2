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
    public void IsReadyToMove(bool isReady)
    {

    }
    public void IsReadyToPlay(bool isReady)
    {

    }
    public void UpdateInputParams(HorizontalInputParams inputParams)
    {

    }

    private void FixedUpdate()
    {
        if (!_isReadyToPlay)
        {
            StopPlayer();
            return;
        }
        if (_isReadyToPlay)
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
        throw new NotImplementedException();
    }

}
