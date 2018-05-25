using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]

public class MeleAttackController : MonoBehaviour {

    public float AttackCooldownTime = 1.3f;
    [Tooltip ("Manager Owner of this Attack Controller")]
    public CreatureManager CharacterManager;

    private Collider _collider;
    private SwordAndShieldAnimationHandler _animator;
    private CreatureManager _AttackCaller;
    private CreatureManager _BlockCaller;
    private bool isInCooldown = false;

    void Awake() {
        _collider = GetComponent<Collider> ();
        _collider.enabled = false;
        _animator = GetComponentInParent<SwordAndShieldAnimationHandler> ();
    }

    public void Attack(CreatureManager caller) {
        if (!isInCooldown) {
            _AttackCaller = caller;
            _animator.CallAttack ();
            StartCoroutine (StartCooldownTimer ());
        } else {
           _AttackCaller.AttackEnd ();
        }
    }

    public void ActivateCollisionAttack() {
        _collider.enabled = true;
    }

    public void DeactivateCollisionAttack() {
        _collider.enabled = false;

        _AttackCaller.AttackEnd ();
    }

    IEnumerator StartCooldownTimer() {
        isInCooldown = true;

        yield return new WaitForSeconds (AttackCooldownTime);

        isInCooldown = false;
    }

    void OnTriggerEnter(Collider other) {
        CreatureManager victimsManager = other.GetComponent<CreatureManager> ();

        if(victimsManager != null) {
            victimsManager.RecieveDamage (CharacterManager.GetDamageDealt (DamageType.Mele));
        }
    }

    //This method stops the 
    public void BlockWithShield(CreatureManager caller) {
        if (isInCooldown) 
            DeactivateCollisionAttack ();

        _BlockCaller = caller;
        _animator.CallShieldAttack ();
    }

    public void EndBlockWithShield() {
        _BlockCaller.BlockEnd ();
    }
}
