using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    public HealthScript healthBar;
    public float speed = 2.0f;
    private CharacterController _charController;

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector3 movement = new Vector3(deltaX, 0, 0);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Fireball")
        {
            Debug.Log(other.gameObject.tag);
            healthBar.Damage();
        }
    }
}
