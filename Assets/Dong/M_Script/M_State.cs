public class M_State
{
    protected M_Base m_base;
    protected M_StateMachine stateMachine;
    string aniBoolName;


    public M_State(M_Base @base, M_StateMachine stateMachine, string aniboolname)
    {
        this.m_base = @base;
        this.stateMachine = stateMachine;
        this.aniBoolName = aniboolname;
    }

    public virtual void Enter()
    {
        m_base.ani.SetBool(aniBoolName, true);
    }
    public virtual void Update()
    {

    }
    public virtual void Exit()
    {
        m_base.ani.SetBool(aniBoolName, false);
    }
}
