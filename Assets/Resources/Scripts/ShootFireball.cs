using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour
{

    private float fireballRange = 10.0f;
    private float fireballSpeed = 5.0f;
    private GameObject fireballPrefab;

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
