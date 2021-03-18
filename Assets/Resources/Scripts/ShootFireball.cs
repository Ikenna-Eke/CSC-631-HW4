using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShootFireball : MonoBehaviour
{

    private float fireballRange = 15.0f;
    private float fireballSpeed = 5.0f;
    private GameObject fireballPrefab;
    private GameObject health;

    void Start()
    {
        fireballPrefab = Resources.Load<GameObject>("Prefabs/Fireball");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShootFireballMethod());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Fireball")
        {
            //health.GetComponent<HealthScript>().Damage();
            Debug.Log("Hit");
        }
    }

    private IEnumerator ShootFireballMethod()
    {
        Vector3 spawnLoc = this.transform.position + this.transform.forward;
        Vector3 fireballDirection = this.transform.forward;
        GameObject tempFireball = Instantiate(fireballPrefab, spawnLoc, Quaternion.identity);

        for (float i = fireballRange; i >= 0; i -= Time.deltaTime)
        {
            tempFireball.transform.Translate(fireballDirection * Time.deltaTime * fireballSpeed);
            yield return null;
        }

        Destroy(tempFireball);
    }
}
