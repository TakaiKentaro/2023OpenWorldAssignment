using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : IActorStatus
{
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Speed { get; set; }
    
    public void SetStatus(int health, int attack, int speed)
    {
        Health = health;
        Attack = attack;
        Speed = speed;
    }
}