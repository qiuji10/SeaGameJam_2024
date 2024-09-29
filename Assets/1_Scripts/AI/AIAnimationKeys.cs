using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIAnimationKeys
{
    public static int ATTACK = Animator.StringToHash("IsAttacking");
    public static int MOVE = Animator.StringToHash("IsMoving");
    public static int DEATH = Animator.StringToHash("IsDeath");

}
