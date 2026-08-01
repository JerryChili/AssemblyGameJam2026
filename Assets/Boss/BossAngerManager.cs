using System;
using UnityEngine;

public class BossAngerManager : MonoBehaviour
{
    public static BossAngerManager Instance;

    [SerializeField]
    private float anger = 0;

    public float huntThreshold = 80;

    public float Anger => anger;

    public bool BossIsOut => anger >= huntThreshold;

    public event Action<float> OnAngerChanged;
    public event Action OnBossReleased;
    public event Action OnBossCalmed;

    private bool bossOut;

    private void Awake()
    {
        Instance = this;
    }

    public void QuotaCompleted()
    {
        RemoveAnger(10);
    }

    public void QuotaFailed(int failedTasks)
    {
        AddAnger(failedTasks * 15);
    }

    public void UrgentCompleted()
    {
        RemoveAnger(5);
    }

    public void UrgentFailed()
    {
        AddAnger(20);
    }

    void AddAnger(float amount)
    {
        anger += amount;
        Debug.Log("Anger now: " + anger);

        OnAngerChanged?.Invoke(anger);

        if (!bossOut && anger >= huntThreshold)
        {
            bossOut = true;
            OnBossReleased?.Invoke();
        }
    }

    void RemoveAnger(float amount)
    {
        anger = Mathf.Max(0, anger - amount);
        Debug.Log("Anger now: " + anger);

        OnAngerChanged?.Invoke(anger);

        if (bossOut && anger < huntThreshold)
        {
            bossOut = false;
            OnBossCalmed?.Invoke();
        }
    }
}