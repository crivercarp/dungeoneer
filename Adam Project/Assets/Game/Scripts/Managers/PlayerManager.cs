using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that define the general behviour of the Player, like current HP
 * */

public class PlayerManager : CreatureManager {

    [Header("Controllers")]
    [SerializeField]
    private MeleAttackController PlayerAttackController;
    [SerializeField]
    private CameraBaseMovement CameraMovement;
    [SerializeField]
    private PlayerParticlesManager ParticlesManager;

    public override void AttackStart(Vector3 Direction) {
        CameraMovement.RotateCharacter (Direction);
        CameraMovement.SetMove (false);

        PlayerAttackController.Attack (this);  
    }

    public override void AttackEnd() {
        CameraMovement.SetMove (true);
    }

    public override void BlockStart(Vector3 Direction) {
        CameraMovement.RotateCharacter (Direction);
        CameraMovement.SetMove (false);

        PlayerAttackController.BlockWithShield (this);
        SetShieldBlocking (true);
    }

    public override void BlockEnd() {
        CameraMovement.SetMove (true);
        SetShieldBlocking (false);
    }

    public override void AttackBlocked() {
        ParticlesManager.PlayShieldParticles ();
    }
}
