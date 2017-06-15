using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject projectileToUse;
    [SerializeField] GameObject projectileSocket;

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float damagePerShot = 9f;
    [SerializeField] float secondsBetweenShots = 0.5f;

    [SerializeField] float attackRadius = 4f;
    [SerializeField] float chaseRadius = 4f;
    float currentHealthPoints = 100f;

    AICharacterControl aiCharacterControl = null;
    GameObject player = null;

    bool isAttacking = false;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("SpawnProjectile", 0f, secondsBetweenShots);  // TODO Switch to Coroutines
        }
        else
        {
            isAttacking = false;
            CancelInvoke();
        }


        if (distanceToPlayer <= chaseRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }

    void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity); // Spawns project at the projectile socket 
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.damageCaused = damagePerShot;

        Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized; // shoots projectile in direction of the player from the projectileSocket
        float projectileSpeed = projectileComponent.projectileSpeed;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
    }

    private void OnDrawGizmos()
    {
        // Draw Move and Attack Gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
