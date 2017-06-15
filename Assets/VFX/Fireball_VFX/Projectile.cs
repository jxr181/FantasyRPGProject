using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float damageCaused = 10f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Component damageableComponent = collider.gameObject.GetComponent(typeof(IDamageable));
        print("damageableComponent = " + damageableComponent);

        if (damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damageCaused);
        }
    }
}
