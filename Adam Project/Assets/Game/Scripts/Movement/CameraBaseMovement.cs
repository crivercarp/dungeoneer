using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SwordAndShieldAnimationHandler))]
[RequireComponent (typeof (Rigidbody))]

public class CameraBaseMovement : MonoBehaviour {

    public enum MovingDirections {
        Up, Down, Right, Left, RightUp, RightDown, DownLeft, LeftUp, None
    }

    [Range (1.0f, 2.0f)]
    public float Speed = 1.8f;
    [Range (5.0f, 8.0f)]
    public float ChargeSpeed = 5;
    public float TurningToMoveSpeed = 6;
    public float TurningToLookSpeed = 25;

    private SwordAndShieldAnimationHandler _animator;
    private Camera CurrentCamera;
    private Rigidbody _rigidBody;
    private Vector3 _targetDir;
    private bool _isCharging = false;
    private bool _isTurningToLook = false;
    private bool _canMove = true;

    void Awake() {
        CurrentCamera = Camera.main;
        _rigidBody = GetComponentInParent<Rigidbody> ();
        _animator = GetComponent<SwordAndShieldAnimationHandler> ();
        
    }

    void Update() {
        float step = _isTurningToLook ? TurningToLookSpeed * Time.deltaTime : TurningToMoveSpeed * Time.deltaTime;

        _targetDir.y = gameObject.transform.position.y;

        if(Vector3.Distance( _targetDir, transform.position) > 0.03 || Vector3.Distance (_targetDir, transform.position) < -0.03) {
            Quaternion wantedRotation = Quaternion.LookRotation (_targetDir - transform.position);
            gameObject.transform.rotation = Quaternion.Lerp (transform.rotation, wantedRotation, step);
        }
            
    }

    public void MoveCharacter(MovingDirections direction) {
        if (_canMove) {
            _isTurningToLook = false;
            switch (direction) {
                case MovingDirections.None:
                    _rigidBody.velocity = Vector3.zero;
                    if (_animator != null)
                        _animator.SetMovement (false);
                    break;
                default:
                    Vector3 finalDirection = GetPositionRelativeToCamera (direction);
                    _targetDir = gameObject.transform.position + finalDirection;
                    _animator.SetMovement (true);

                    if (!_isCharging) {
                        _rigidBody.velocity = (finalDirection * Speed);
                        _animator.SetCharge (false);
                        _animator.SetSpeed (Speed);
                    } else {
                        _rigidBody.velocity = (finalDirection * ChargeSpeed);
                        _animator.SetCharge (true);
                    }
                    break;
            }
        }
    }

    private Vector3 GetPositionRelativeToCamera(MovingDirections originalDirection) {
        Vector3 result = Vector3.zero;

        switch (originalDirection) {
            case MovingDirections.Up:
                result = CurrentCamera.transform.forward;
                break;
            case MovingDirections.Down:
                result = CurrentCamera.transform.forward * -1;
                break;
            case MovingDirections.Left:
                result = CurrentCamera.transform.right * -1;
                break;
            case MovingDirections.Right:
                result = CurrentCamera.transform.right;
                break;
            case MovingDirections.RightUp:
                result = CurrentCamera.transform.right + CurrentCamera.transform.forward;
                break;
            case MovingDirections.RightDown:
                result = CurrentCamera.transform.right + (CurrentCamera.transform.forward * -1);
                break;
            case MovingDirections.DownLeft:
                result = (CurrentCamera.transform.forward * -1) + (CurrentCamera.transform.right * -1);
                break;
            case MovingDirections.LeftUp:
                result = (CurrentCamera.transform.right * -1) + CurrentCamera.transform.forward;
                break;
        }

        result.y = 0;
        result.Normalize ();
        return result;
    }

    public void RotateCharacter(Vector3 lookAtPlace) {
        if (_canMove) {
            _isTurningToLook = true;
            lookAtPlace.y = gameObject.transform.localPosition.y;
            _targetDir = lookAtPlace;
        }
    }

    public void SetCharge(bool IsCharging) {
        this._isCharging = IsCharging;
    }

    public void SetMove (bool canMove) {
        _canMove = canMove;
        _animator.SetCanMove (canMove);
    }
}
