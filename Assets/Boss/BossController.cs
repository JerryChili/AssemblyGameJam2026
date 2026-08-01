using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public Transform officePoint;

    [Header("Visual Components")]
    public Renderer[] bossRenderers;
    public Collider[] bossColliders;
    public Animator animator;

    [Header("Hunting")]
    public float minimumSpeed = 2f;
    public float maximumSpeed = 7f;

    public float angerForMaxSpeed = 150f;

    public float catchDistance = 1.5f;

    private bool hunting;
    private bool returning;

    private AudioSource ambience;

    private void Awake()
    {
        ambience = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BossAngerManager.Instance.OnBossReleased += BeginHunt;
        BossAngerManager.Instance.OnBossCalmed += ReturnToOffice;


        HideBoss();
    }


    private void Update()
    {
        if (hunting)
        {
            UpdateBossSpeed();

            agent.SetDestination(player.position);


            float distance =
                Vector3.Distance(
                    transform.position,
                    player.position
                );


            if (distance <= catchDistance)
            {
                PlayerCaught();
            }
        }


        if (returning)
        {
            if (!agent.pathPending &&
                agent.remainingDistance <= agent.stoppingDistance)
            {
                returning = false;

                HideBoss();
            }
        }
    }

    private void PlayerCaught()
    {
        Debug.Log("PLAYER CAUGHT");
        gameObject.SetActive(false);
        GameOverManager.Instance.GameOver();
    }

    private void UpdateBossSpeed()
    {
        float anger = BossAngerManager.Instance.Anger;


        float angerPercentage = Mathf.InverseLerp(
            80f,
            angerForMaxSpeed,
            anger
        );


        agent.speed = Mathf.Lerp(
            minimumSpeed,
            maximumSpeed,
            angerPercentage
        );
    }

    private void BeginHunt()
    {
        ShowBoss();

        hunting = true;
        returning = false;

        agent.enabled = true;

        agent.SetDestination(player.position);

        ambience.Play();
        Debug.Log("Boss released!");
    }


    private void ReturnToOffice()
    {
        hunting = false;
        returning = true;


        agent.SetDestination(officePoint.position);

        ambience.Stop();
        Debug.Log("Boss returning...");
    }


    private void ShowBoss()
    {
        foreach (Renderer r in bossRenderers)
            r.enabled = true;


        foreach (Collider c in bossColliders)
            c.enabled = true;


        if (animator != null)
            animator.enabled = true;
    }


    private void HideBoss()
    {
        hunting = false;
        returning = false;


        if (agent.enabled)
            agent.enabled = false;


        foreach (Renderer r in bossRenderers)
            r.enabled = false;


        foreach (Collider c in bossColliders)
            c.enabled = false;


        if (animator != null)
            animator.enabled = false;


        transform.position = officePoint.position;
    }
}