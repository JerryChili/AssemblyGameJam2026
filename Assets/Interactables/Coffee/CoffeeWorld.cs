using UnityEngine;

public class CoffeeWorld : MonoBehaviour
{
    public ParticleSystem spillEffect;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1f)
        {
            SpillCoffee();
        }
    }


    private void SpillCoffee()
    {
        if (spillEffect != null)
        {
            Instantiate(
                spillEffect,
                transform.position,
                Quaternion.identity
            );
        }


        Destroy(gameObject);
    }
}