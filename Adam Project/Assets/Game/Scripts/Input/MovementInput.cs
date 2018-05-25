using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CameraBaseMovement))]

public class MovementInput : MonoBehaviour {

    private CameraBaseMovement _movementScript;
    private bool CanMove = true;

    void Awake() {
        _movementScript = GetComponent<CameraBaseMovement> ();
    }

    // Update is called once per frame
    void Update() {
        if (CanMove) {
            if (Input.GetKeyDown (KeyCode.LeftShift))
                _movementScript.SetCharge (true);
            if (Input.GetKeyUp (KeyCode.LeftShift))
                _movementScript.SetCharge (false);

            if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) {
                _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.RightUp);
            } else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
                _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.RightDown);
            } else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
                _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.DownLeft);
            } else if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.W)) {
                _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.LeftUp);
            } else {
                if (Input.GetKey (KeyCode.W)) {
                    _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.Up);
                }
                if (Input.GetKey (KeyCode.D)) {
                    _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.Right);
                }
                if (Input.GetKey (KeyCode.A)) {
                    _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.Left);
                }
                if (Input.GetKey (KeyCode.S)) {
                    _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.Down);
                }
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.D)) {
                _movementScript.MoveCharacter (CameraBaseMovement.MovingDirections.None);
            }
        }
    }

    public void SetMovement(bool newMovement) {
        CanMove = newMovement;
    }
}
