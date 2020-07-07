using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public static int collisionCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hm = Input.GetAxis("Horizontal");
        float vm = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hm, 0, vm);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            collisionCount++;
        
    }
}
