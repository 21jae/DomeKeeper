public class M_FlyerHit : M_State
{
    M_Flyer flyer;
    public M_FlyerHit(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Flyer flyer) : base(@base, stateMachine, aniboolname)
    {
        this.flyer = flyer;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!flyer.attacked) stateMachine.ChangeState(flyer.attack);
    }
}
