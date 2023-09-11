using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_DiverWarner : M_Moving
{
    public O_DiverWarnerActive active { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        active = new O_DiverWarnerActive(this, stateMachine, "Active");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initiate(active);
           

    }

    protected override void Update()
    {
        base.Update();
        transform.Translate(Vector2.down * Time.deltaTime * moveSpeed.x);
    }
}
