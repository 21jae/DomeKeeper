using UnityEngine;

public class M_DiverAttack : M_State
{
    M_Diver diver;
    Vector2 atk;
    public M_DiverAttack(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Diver diver) : base(@base, stateMachine, aniboolname)
    {
        this.diver = diver;
    }

    Vector2 dir;

    public override void Enter()
    {
        base.Enter();

        diver.isAttacking = 1;
        diver.SoundDive();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        diver.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        diver.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        diver.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;


        if (diver.transform.position != null)
        {
            atk = diver.Getdir();
            atk = atk.normalized;
            diver.SetVecVelocity(atk.x * diver.moveSpeed.x, atk.y * diver.moveSpeed.x);
        }
    }
}
