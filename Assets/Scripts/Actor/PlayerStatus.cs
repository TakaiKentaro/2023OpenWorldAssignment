using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : IActorStatus
{
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    
    public void SetStatus(int health, int attack, int defense, int speed)
    {
        Health = health;
        Attack = attack;
        Defense = defense;
        Speed = speed;
    }
}