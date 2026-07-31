using UnityEngine;

public class Spill : MonoBehaviour
{
    private int cycles;
    private int maxCycles;

    private Vector3 initialScale;

    private void Start()
    {
        maxCycles = Random.Range(10, 30);
        cycles = maxCycles;

        initialScale = transform.localScale;
    }

    public void Clean()
    {
        cycles--;

        // Remaining percentage (1 -> 0)
        float remaining = (float)cycles / maxCycles;
        float scale = Mathf.Lerp(0.2f, 1f, remaining);

        transform.localScale = initialScale * scale;

        Debug.Log($"Cycles left: {cycles}/{maxCycles}");

        if (cycles <= 0)
        {
            Debug.Log("Cleaned!");
            //spillTask.SpillCleaned();
            Destroy(gameObject);
        }
    }
}