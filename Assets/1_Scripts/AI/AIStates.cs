using UnityEngine;
using Random = UnityEngine.Random;

public class IdleState : AIStateBase
{
    public IdleState(AIBlackboard blackboard) : base(blackboard) { }

    protected override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}

public class PatrolState : AIStateBase
{
    public PatrolState(AIBlackboard blackboard) : base(blackboard) { }

    protected override void OnEnter()
    {
        base.OnEnter();

        _blackboard.speed = _blackboard.baseSpeed;

        int previousIndex = _blackboard.destinationIndex;
        int newIndex;

        do { newIndex = Random.Range(0, _blackboard.waypoints.Count); }
        while (newIndex == previousIndex);

        _blackboard.destinationIndex = newIndex;
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        Transform destination = _blackboard.waypoints[_blackboard.destinationIndex];
        Vector2 newPosition = _blackboard.agentTransform.CalculateMovePosition(destination.position);

        _blackboard.direction = _blackboard.agentTransform.GetDirection(newPosition);
        _blackboard.rb.MovePosition(newPosition);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    public bool ReachedDestination()
    {
        Transform destination = _blackboard.waypoints[_blackboard.destinationIndex];
        Vector2 currentPosition = _blackboard.rb.position;
        Vector2 targetPosition = destination.position;

        return Vector2.Distance(currentPosition, targetPosition) < 0.2f;
    }
}

public class AlertState : AIStateBase
{
    public AlertState(AIBlackboard blackboard) : base(blackboard) { }

    protected override void OnEnter()
    {
        base.OnEnter();

        _blackboard.speed = _blackboard.chaseSpeed;
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        Transform destination = _blackboard.alertDetection.target.transform;
        Vector2 newPosition = _blackboard.agentTransform.CalculateMovePosition(destination.position);

        newPosition = new Vector2(newPosition.x, _blackboard.rb.position.y);

        _blackboard.direction = _blackboard.agentTransform.GetDirection(newPosition);
        _blackboard.rb.MovePosition(newPosition);
    }

    protected override void OnExit()
    {
        base.OnExit();

        _blackboard.speed = _blackboard.baseSpeed;
    }
}

public class AttackState : AIStateBase
{
    public AttackState(AIBlackboard blackboard) : base(blackboard) { }

    protected override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}

public class DeathState : AIStateBase
{
    public DeathState(AIBlackboard blackboard) : base(blackboard) { }

    protected override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}