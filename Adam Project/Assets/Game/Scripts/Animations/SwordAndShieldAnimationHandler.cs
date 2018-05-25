using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class In charge to interact with the animator and call the different animations of the characters with a Sword and a Shield, like 
 * the Knight_01 and the Skeleton_01
 * */

public class SwordAndShieldAnimationHandler : MonoBehaviour {

    private Animator _animator;

    // Use this for initialization
    void Awake() {
        _animator = GetComponentInChildren<Animator> ();
    }

    public void SetMovement (bool IsMooving) {
        _animator.SetBool ("IsMoving", IsMooving);
    }

    public void SetCharge (bool IsCharging) {
        _animator.SetBool ("IsCharging", IsCharging);
    }

    public void SetSpeed (float Speed) {
        _animator.SetFloat ("Speed", Speed);
    }

    public void CallAttack() {
        _animator.SetTrigger ("Attack_T");
    }

    public void CallShieldAttack() {
        _animator.SetTrigger ("Block_T");
    }

    public void SetCanMove(bool CanMove) {
        _animator.SetBool ("CanMove", CanMove);
    }
}
