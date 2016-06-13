using UnityEngine;
using System.Collections;

public class LeftStick : MonoBehaviour {

    public Rigidbody rb;
    

    public float force;

    Vector3 leftStick;


    void Start()
    {
        force = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        leftStick = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        rb.AddForce(transform.position + (leftStick * force)/Time.smoothDeltaTime);
        //rb.AddForce(transform.up * upforce / Time.deltaTime);
    }
}
