using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : ActorStatusBase
{
    public PlayerStatus(int initialHp, int initialAttack, int initialDefense, int initialAgility)
        : base(initialHp, initialAttack, initialDefense, initialAgility)
    {
    }
}