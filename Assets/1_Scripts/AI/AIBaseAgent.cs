using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBaseAgent : MonoBehaviour
{
    [SerializeField] AIBlackboard _blackboard;
    private readonly StateMachine sm = new StateMachine();

    private List<AIStateBase> states = new List<AIStateBase>();

    public void Awake()
    {
        var idle = new IdleState(_blackboard);
        var patrol = new PatrolState(_blackboard);
        var alert = new AlertState(_blackboard);
        var attack = new AttackState(_blackboard);
        var death = new DeathState(_blackboard);

        states = new List<AIStateBase> { idle, patrol, alert, attack, death };

        idle.AddTransition(patrol, timeInState => timeInState > 1f);
        idle.AddTransition(alert, timeInState => _blackboard.alertDetection.detected);
        idle.AddTransition(attack, timeInState => _blackboard.attackDetection.detected);

        patrol.AddTransition(idle, timeInState => patrol.ReachedDestination());
        patrol.AddTransition(alert, timeInState => _blackboard.alertDetection.detected);
        patrol.AddTransition(attack, timeInState => _blackboard.attackDetection.detected);

        alert.AddTransition(idle, timeInState => !_blackboard.alertDetection.detected);
        alert.AddTransition(attack, timeInState => _blackboard.attackDetection.detected);

        attack.AddTransition(idle, timeInState => !_blackboard.attackDetection.detected);

        FromAnyStateTo(death, timeInState => _blackboard.health.IsDead());

        sm.SetInitialState(idle);
    }

    protected void FromAnyStateTo(AIStateBase toState, Func<float, bool> predicate, params AIStateBase[] excludedStates)
    {
        if (toState == null)
        {
            Debug.LogError($"State {toState.GetType()} is null");
            return;
        }

        foreach (var state in states)
        {
            if (state == toState || InExclude(state))
                continue;

            state.AddTransition(toState, predicate);
        }

        bool InExclude(AIStateBase state)
        {
            for (int i = 0; i < excludedStates.Length; i++)
            {
                if (excludedStates[i] == state)
                    return true;
            }

            return false;
        }
    }

    public void Update()
    {
        sm.Tick(Time.deltaTime);
    }
}
