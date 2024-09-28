using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateBase : StateBase
{
    public override string Name => GetType().Name;

    protected AIBlackboard _blackboard;

    public AIStateBase(AIBlackboard blackboard)
    {
        _blackboard = blackboard;
    }
}
