using UnityEngine;
using UnityEngine.UI;

public class StaminaVignette : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private Image vignette;

    private void Awake()
    {
        vignette = GetComponent<Image>();
    }

    private void Update()
    {
        // 0 stamina = 1 exhaustion, full stamina = 0 exhaustion
        float exhaustion = 1f - playerMovement.StaminaPercent;

        Color color = vignette.color;
        color.a = exhaustion;

        vignette.color = color;
    }
}