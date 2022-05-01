using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    public int health { get; protected set; }
    public int damage { get; protected set; }

    public abstract void takeDamage(int damage);
    public abstract void die();

    public int getHealt()
    {
        return health;
    }
    public int getDamage()
    {
        return damage;
    }

}
