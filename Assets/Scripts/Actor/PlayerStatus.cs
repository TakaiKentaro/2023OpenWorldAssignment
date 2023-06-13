using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : ActorStatusBase
{
    public PlayerStatus(int initialHp, int initialAttack, int defense, int agility)
        : base(initialHp, initialAttack, defense, agility)
    {
    }
}