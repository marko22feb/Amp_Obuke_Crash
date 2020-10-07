using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public enum UnitType { Player, Mob, Elite, Boss };
    public UnitType type;
    public void Damage()
    {
        switch (type)
        {
            case UnitType.Player:
                if (GameController.control.extraLives > 0)
                {
                    GameController.control.AddExtraLives(-1);
                } else
                {
                    PlayerController PC = GameController.control.Player.GetComponent<PlayerController>();
                    PC.PlayAnim("CrashDeath", PC.GroanSounds);
                }
                break;
            case UnitType.Mob:
                break;
            case UnitType.Elite:
                break;
            case UnitType.Boss:
                break;
            default:
                break;
        }
    }
}
