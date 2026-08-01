using UnityEngine;

public class OfficeLightController : MonoBehaviour
{
    [Header("Boss Office Lights")]
    public Renderer[] bossOfficeRenderers;

    [Header("Office Lights")]
    public Renderer[] officeRenderers;

    [Header("Colors")]
    public Color calmColor = Color.white;
    public Color angryColor = Color.red;

    [Header("Emission Strength")]
    public float emissionIntensity = 2f;


    private Material[] bossOfficeMaterials;
    private Material[] officeMaterials;

    private Color[] bossOriginalEmission;
    private Color[] officeOriginalEmission;


    private void Start()
    {
        // Boss office materials
        bossOfficeMaterials = new Material[bossOfficeRenderers.Length];
        bossOriginalEmission = new Color[bossOfficeRenderers.Length];

        for (int i = 0; i < bossOfficeRenderers.Length; i++)
        {
            if (bossOfficeRenderers[i] == null)
                continue;


            bossOfficeMaterials[i] =
                bossOfficeRenderers[i].material;


            EnableEmission(
                bossOfficeMaterials[i]
            );


            bossOriginalEmission[i] =
                bossOfficeMaterials[i]
                .GetColor("_EmissionColor");
        }


        // Main office materials
        officeMaterials = new Material[officeRenderers.Length];
        officeOriginalEmission = new Color[officeRenderers.Length];


        for (int i = 0; i < officeRenderers.Length; i++)
        {
            if (officeRenderers[i] == null)
                continue;


            officeMaterials[i] =
                officeRenderers[i].material;


            EnableEmission(
                officeMaterials[i]
            );


            officeOriginalEmission[i] =
                officeMaterials[i]
                .GetColor("_EmissionColor");
        }


        BossAngerManager.Instance.OnAngerChanged += UpdateOfficeLights;
        BossAngerManager.Instance.OnBossReleased += BossReleased;
        BossAngerManager.Instance.OnBossCalmed += BossReturned;

    }

    private void UpdateOfficeLights(float anger)
    {
        float t = Mathf.Clamp01(
            anger / BossAngerManager.Instance.huntThreshold
        );


        Color emission =
            Color.Lerp(
                calmColor,
                angryColor,
                t
            );


        foreach (Material material in bossOfficeMaterials)
        {
            if (material == null)
                continue;


            SetEmission(
                material,
                emission
            );
        }
    }



    private void BossReleased()
    {
        foreach (Material material in officeMaterials)
        {
            if (material == null)
                continue;


            SetEmission(
                material,
                angryColor
            );
        }
    }



    private void BossReturned()
    {
        for (int i = 0; i < officeMaterials.Length; i++)
        {
            if (officeMaterials[i] == null)
                continue;


            SetEmission(
                officeMaterials[i],
                officeOriginalEmission[i]
            );
        }


        for (int i = 0; i < bossOfficeMaterials.Length; i++)
        {
            if (bossOfficeMaterials[i] == null)
                continue;


            SetEmission(
                bossOfficeMaterials[i],
                bossOriginalEmission[i]
            );
        }
    }

    private void TestEmission()
    {
        foreach (Material material in bossOfficeMaterials)
        {
            if (material == null)
                continue;

            material.EnableKeyword("_EMISSION");

            material.SetColor(
                "_EmissionColor",
                Color.magenta * 10f
            );
        }
    }

    private void SetEmission(
        Material material,
        Color color
    )
    {
        material.EnableKeyword("_EMISSION");

        material.SetColor(
            "_EmissionColor",
            color * emissionIntensity
        );
    }



    private void EnableEmission(
        Material material
    )
    {
        material.EnableKeyword("_EMISSION");
    }



    private void OnDestroy()
    {
        if (BossAngerManager.Instance == null)
            return;


        BossAngerManager.Instance.OnAngerChanged -= UpdateOfficeLights;
        BossAngerManager.Instance.OnBossReleased -= BossReleased;
        BossAngerManager.Instance.OnBossCalmed -= BossReturned;
    }
}