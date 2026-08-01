using UnityEngine;

public class OfficeLightController : MonoBehaviour
{
    public Light officeLight;

    public Light[] officeLights;

    public Color calmColor = Color.white;
    public Color angryColor = Color.red;

    private Color[] originalColors;

    private void Start()
    {
        originalColors = new Color[officeLights.Length];

        for (int i = 0; i < officeLights.Length; i++)
            originalColors[i] = officeLights[i].color;

        BossAngerManager.Instance.OnAngerChanged += UpdateOfficeLight;
        BossAngerManager.Instance.OnBossReleased += BossReleased;
        BossAngerManager.Instance.OnBossCalmed += BossReturned;
    }

    void UpdateOfficeLight(float anger)
    {
        float t = Mathf.Clamp01(
            anger / BossAngerManager.Instance.huntThreshold
        );

        officeLight.color =
            Color.Lerp(calmColor, angryColor, t);
    }

    void BossReleased()
    {
        foreach (Light light in officeLights)
            light.color = Color.red;
    }

    void BossReturned()
    {
        for (int i = 0; i < officeLights.Length; i++)
            officeLights[i].color = originalColors[i];
    }
}
