using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBaseAgent : MonoBehaviour
{
    [SerializeField] AIBlackboard _blackboard;
    private StateMachine sm;

    public void Awake()
    {
        var idle = new IdleState(_blackboard);
        var move = new MoveState(_blackboard);
        var detect = new DetectState(_blackboard);
        var attack = new AttackState(_blackboard);
        var death = new DeathState(_blackboard);

        
    }
}
