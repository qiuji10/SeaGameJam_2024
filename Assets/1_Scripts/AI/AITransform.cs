using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIAnimationKeys;

public class AITransform : MonoBehaviour
{
    private AIBlackboard _blackboard;
    private Vector3 prevPosition;

    private void Awake()
    {
        _blackboard = GetComponent<AIBlackboard>();
    }

    private void Update()
    {
        UpdateFaceDirection();
    }

    private void FixedUpdate()
    {
        UpdateWalkingAnimation();
    }

    public void UpdateWalkingAnimation()
    {
        bool isMoving = transform.position != prevPosition;
        _blackboard.animator.SetBool(MOVE, isMoving);
        prevPosition = transform.position;
    }

    public void UpdateFaceDirection()
    {
        if (_blackboard.direction == EDirection.Left)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public EDirection GetDirection(Vector3 position)
    {
        return transform.position.x >= position.x ? EDirection.Left : EDirection.Right;
    }

    public Vector3 CalculateMovePosition(Vector2 targetPosition)
    {
        Vector2 currentPosition = _blackboard.rb.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;

        float moveSpeed = _blackboard.speed;
        return currentPosition + direction * moveSpeed * Time.fixedDeltaTime;
    }
}
