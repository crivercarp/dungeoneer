using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]

public class AIBasicEnemyMovement : MonoBehaviour {

    public float MeleAttackRange = 2f;
    public float RotationSpeed = 2f;

    private SimpleEnemyManager _enemyManager;
    private NavMeshAgent _agent;
    private bool _canMove = true;
    private SwordAndShieldAnimationHandler _animator;

    void Awake() {
        _agent = GetComponent<NavMeshAgent> ();
        _animator = GetComponent<SwordAndShieldAnimationHandler> ();
        _enemyManager = GetComponentInParent<SimpleEnemyManager> ();
    }
	
	void Update () {
        Transform playerPosition = GameMaster.Instance.GetPlayer ().transform;
        RotateTowards (playerPosition);

        //If Can move, move to the player's position
        if (_canMove) {
            _agent.isStopped = false;

            if (_agent.remainingDistance > _agent.stoppingDistance) {

                _animator.SetMovement (true);
                _animator.SetSpeed (_agent.speed);
            }else {
                _animator.SetMovement (false);
            }

            _agent.destination = playerPosition.position;
        // If not, stop
        } else {
            _animator.SetMovement (false);
            _agent.isStopped = true;
        }

        //If is in rage, always look at him and Attack him
        if (IsInMeleeRangeOf (playerPosition)) {
            _enemyManager.AttackStart(Vector3.zero);
        }
    }

    public void SetMove(bool newMove) {
        _canMove = newMove;
    }

    private void RotateTowards(Transform target) {
        var neededRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * RotationSpeed);

        transform.eulerAngles = new Vector3 (0,transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private bool IsInMeleeRangeOf(Transform target) {
        return MeleAttackRange >= _agent.remainingDistance;
    }
}
