using UnityEngine;
using Random = UnityEngine.Random;
using static AIAnimationKeys;

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

        GameObject.Instantiate(_blackboard.warningPopUp, _blackboard.transform.position + new Vector3(0, 1, 0), Quaternion.identity, _blackboard.transform);

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
        _blackboard.target = _blackboard.attackDetection.target.transform;
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        Transform weaponTR = _blackboard.weaponGameObject.transform;
        IWeapon weapon = _blackboard.weapon;

        Vector3 direction = _blackboard.target.position - weaponTR.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        weaponTR.rotation = Quaternion.Euler(0, 0, angle);

        if (weapon.CanAttack())
        {
            _blackboard.animator.SetTrigger(ATTACK);
            weapon.Attack();
        }
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

        if (TimeInState > 1)
        {
            GameObject.Destroy(_blackboard.gameObject);
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}