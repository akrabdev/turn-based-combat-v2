using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem effect;
    public string soundEffectName;
    bool isFiring = false;
    Unit firer;
    Unit target;
    Vector3 projDestination;
    Vector3 projDirection;
    int damage;
    Element element;
    bool follow;
    int followSpeed;
    public int multipleNumber;
    public float counter = 0f;

    //private void Start()
    //{
    //    Destroy(gameObject, 5f);
    //}

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
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

    public void Fire(Unit firerInput, Unit targetInput, int damageInput, Element elementInput, bool followInput, int followSpeedInput, bool multiple = false, int multipleNumberInput = 0)
    {
        firer = firerInput;
        target = targetInput;
        damage = damageInput;
        element = elementInput;
        follow = followInput;
        followSpeed = followSpeedInput;
        projDirection = (target.transform.position - firer.transform.position).normalized;
        projDestination = target.transform.position;
        multipleNumber = multipleNumberInput;
        if (multiple)
        {
            for (int i = 1; i < multipleNumber; i++)
            {
                Debug.Log(i);
                Invoke("FireMore", 0.1f * i);
            }
        }
        isFiring = true;
        
    }

    private void FireMore()
    {
        GameObject instantiatedProj = Instantiate(gameObject, firer.transform.position, Quaternion.identity);
        Projectile instantiatedProjComponent = instantiatedProj.GetComponent<Projectile>();
        instantiatedProjComponent.Fire(firer, target, damage, element, follow, followSpeed);
        FindObjectOfType<AudioManager>().Play(soundEffectName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() == target)
        {
            FindObjectOfType<AudioManager>().Play(soundEffectName);
            isFiring = false;
            target.TakeDamage(damage, firer, element);
            Instantiate(effect, target.transform.position, Quaternion.identity);
            if (multipleNumber > 0)
                Destroy(gameObject, (0.1f * multipleNumber) - counter);
            else
                Destroy(gameObject);

        }
        
    }
}
