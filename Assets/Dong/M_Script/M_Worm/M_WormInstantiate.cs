using UnityEngine;

public class M_WormInstantiate : M_State
{
    M_Worm worm;
    float counter;
    float count;
    public M_WormInstantiate(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Worm worm) : base(@base, stateMachine, aniboolname)
    {
        this.worm = worm;
    }

    public override void Enter()
    {
        base.Enter();
        counter = Random.Range(worm.wakeUpTimer_Min, worm.wakeUpTimer_Max + 1);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        count += Time.deltaTime;
        if (counter <= count)
        {
            stateMachine.ChangeState(worm.wakeUp);
        }

    }
}
