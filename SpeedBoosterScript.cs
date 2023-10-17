using UnityEngine;
using UnityEngine.AI;

public class SpeedBoost : MonoBehaviour
{
    public float boostMultiplier = 3f;
    public float boostDuration = 5f;

    private NavMeshAgent agent;
    private float originalSpeed;
    private float originalAcceleration;
    private float boostEndTime;

    public GameObject orb;
    private float orbRespawnTime;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Grab the NavMesh Agent component of the player
            agent = other.GetComponent<NavMeshAgent>();

            // Grab the original settings for speed & acceleration
            originalSpeed = agent.speed;
            originalAcceleration = agent.acceleration;

            // Apply the boost multiplier to speed & acceleration
            agent.speed *= boostMultiplier;
            agent.acceleration *= boostMultiplier;

            // Set the end times for boost and orbRespawn
            boostEndTime = Time.time + boostDuration;
            orbRespawnTime = Time.time + 10f;

            // Make the orb disappear
            orb.SetActive(false);
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }


    private void Update()
    {
        if (agent != null && Time.time >= boostEndTime)
        {
            // Set everything back to normal
            agent.speed = originalSpeed;
            agent.acceleration = originalAcceleration;
            agent = null;  // Reset agent reference to end the check
        }

        if (Time.time >= orbRespawnTime)
        {
            // Make the orb reappear
            orb.SetActive(true);
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
