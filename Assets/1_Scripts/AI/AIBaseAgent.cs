using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBaseAgent : MonoBehaviour
{
    [SerializeField] AIBlackboard _blackboard;
    private readonly StateMachine sm = new StateMachine();

    public void Awake()
    {
        var idle = new IdleState(_blackboard);
        var patrol = new PatrolState(_blackboard);
        var alert = new AlertState(_blackboard);
        var attack = new AttackState(_blackboard);
        var death = new DeathState(_blackboard);

        idle.AddTransition(patrol, timeInState => timeInState > 1f);
        idle.AddTransition(alert, timeInState => _blackboard.alertDetection.detected);

        patrol.AddTransition(idle, timeInState => patrol.ReachedDestination());
        patrol.AddTransition(alert, timeInState => _blackboard.alertDetection.detected);

        alert.AddTransition(idle, timeInState => !_blackboard.alertDetection.detected);

        sm.SetInitialState(idle);
    }

    public void Update()
    {
        sm.Tick(Time.deltaTime);
    }
}
