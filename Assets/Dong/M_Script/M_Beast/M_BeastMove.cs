using Unity.VisualScripting;
using UnityEngine;

public class M_BeastMove : M_State
{
    Rigidbody2D rb;
    M_Beast beast;
    public M_BeastMove(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Beast beast) : base(@base, stateMachine, aniboolname)
    {
        this.beast = beast;
    }

    public override void Enter()
    {
        base.Enter();
        rb = beast.rb;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(beast.domeCenter.position, beast.transform.position) <= beast.howlX && beast.howl == false)
        {
            stateMachine.ChangeState(beast.howling);
        }

        else if (Vector2.Distance(beast.domeCenter.position, beast.transform.position) <= beast.JumpX)
        {
            beast.SetVelocity(beast.zero);
            stateMachine.ChangeState(beast.jump);
        }
        else
        {
            beast.SetVelocity(beast.moveSpeed);
        }

    }
}
