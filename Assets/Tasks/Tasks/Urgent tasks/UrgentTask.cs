using System;
using UnityEngine;


public abstract class UrgentTask : Task
{
    [Header("Urgent Task")]
    public float timeLimit = 60f;


    private float timer;


    public float RemainingTime => timer;


    public Action<UrgentTask> OnUrgentTaskUpdated;


    public bool Failed { get; private set; }



    protected override void Start()
    {
        UrgentTaskManager.Instance.RegisterUrgentTask(this);
    }



    public override void Activate()
    {
        base.Activate();


        timer = timeLimit;
        Failed = false;


        OnUrgentTaskUpdated?.Invoke(this);
    }



    private void Update()
    {
        if (!IsActive || IsComplete || Failed)
            return;


        timer -= Time.deltaTime;


        OnUrgentTaskUpdated?.Invoke(this);



        if (timer <= 0)
        {
            Fail();
        }
    }



    protected virtual void Fail()
    {
        Failed = true;


        Debug.Log(
            $"Urgent task failed: {taskName}"
        );


        OnUrgentTaskUpdated?.Invoke(this);


        UrgentTaskManager.Instance.TaskFailed(this);


        Deactivate();
    }
}