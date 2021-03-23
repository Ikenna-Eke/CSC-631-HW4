using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ShootFireball : MonoBehaviour
{
    private float fireballRange = 6.0f;
    private float fireballSpeed = 5.0f;
    private GameObject fireballPrefab;
    private Boolean justShot = false;
    
    void Start()
    {
        fireballPrefab = Resources.Load<GameObject>("Prefabs/Fireball");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (justShot == false)
            {
                StartCoroutine(ShootFireballMethod());
                justShot = true;
                StartCoroutine(Wait());
                justShot = false;
            }
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "FireBall"){
            gameObject.SetActive(false);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10000);
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
