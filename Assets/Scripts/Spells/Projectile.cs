using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem effect;
    bool isFiring = false;
    Unit firer;
    Unit target;
    Vector3 projDestination;
    Vector3 projDirection;
    int damage;
    bool follow;
    int followSpeed;

    //private void Start()
    //{
    //    Destroy(gameObject, 5f);
    //}

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            if (follow)
            {
                projDestination = target.transform.position;
                projDirection = (target.transform.position - firer.transform.position).normalized;
            }
            transform.Translate((projDestination - transform.position).normalized * followSpeed * Time.deltaTime, Space.World);
            transform.up = projDirection;
            if (Vector3.Distance(projDestination, transform.position) <= 0.2)
                Destroy(gameObject);
        }
    }

    public void Fire(Unit firerInput, Unit targetInput, int damageInput, bool followInput, int followSpeedInput)
    {
        firer = firerInput;
        target = targetInput;
        damage = damageInput;
        follow = followInput;
        followSpeed = followSpeedInput;
        projDirection = (target.transform.position - firer.transform.position).normalized;
        projDestination = target.transform.position;
        isFiring = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() == target)
        {
            Instantiate(effect, target.transform.position, Quaternion.identity);
            isFiring = false;
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
