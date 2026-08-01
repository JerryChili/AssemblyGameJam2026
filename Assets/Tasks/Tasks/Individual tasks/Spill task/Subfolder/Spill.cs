using UnityEngine;
using System;

public class Spill : MonoBehaviour
{
    private int cycles;
    private int maxCycles;

    private Vector3 initialScale;

    public Action OnCleaned;


    private void Awake()
    {
        initialScale = transform.localScale;
        gameObject.SetActive(false);
    }


    public void Activate()
    {
        maxCycles = UnityEngine.Random.Range(10, 30);
        cycles = maxCycles;

        ResetVisual();

        gameObject.SetActive(true);
    }


    private void ResetVisual()
    {
        transform.localScale = initialScale;
    }


    public void Clean()
    {
        if (cycles <= 0)
            return;


        cycles--;


        float remaining = (float)cycles / maxCycles;

        float scale = Mathf.Lerp(
            0.2f,
            1f,
            remaining
        );


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