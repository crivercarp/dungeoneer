using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyManager : CreatureManager {

    [Header("Controllers")]
    [SerializeField]
    private MeleAttackController _EnemyAttackController;

    public override void AttackEnd() {
        // TODO should something be here?
    }

    public override void AttackStart(Vector3 Direction) {
        if (_EnemyAttackController != null)
            _EnemyAttackController.Attack (this);
    }

    public override void BlockEnd() {
        //TODO Add AI for Blocking
    }

    public override void BlockStart(Vector3 Direction) {
        //TODO Add AI for Blocking
    }

    public override void AttackBlocked() {
        //TODO Add AI for Blocking  
        throw new NotImplementedException ();
    }
}
