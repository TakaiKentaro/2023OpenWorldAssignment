using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : ActorStatusBase
{
    public EnemyStatus(int hp, int attack, int initialDefense, int initialAgility)
        : base(hp, attack, initialDefense, initialAgility)
    {
        
    }
}