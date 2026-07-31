using UnityEngine;
using System;
using System.Collections.Generic;

public class Spill : MonoBehaviour
{
    private int cycles;
    private int maxCycles;

    private Vector3 initialScale;

    public Action OnCleaned;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    public void Activate()
    {
        maxCycles = UnityEngine.Random.Range(10, 30);
        cycles = maxCycles;

        transform.localScale = initialScale;

        gameObject.SetActive(true);
    }


    public void Clean()
    {
        if (cycles <= 0)
            return;

        cycles--;

        float remaining = (float)cycles / maxCycles;

        // Shrink between 100% and 20%
        float scale = Mathf.Lerp(0.2f, 1f, remaining);

        transform.localScale = initialScale * scale;

        Debug.Log($"Cycles left: {cycles}/{maxCycles}");


        if (cycles <= 0)
        {
            Debug.Log("Spill cleaned!");

            OnCleaned?.Invoke();

            gameObject.SetActive(false);
        }
    }
}