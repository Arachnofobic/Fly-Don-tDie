using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float thrust;
    [SerializeField] float rotationThrust;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }
    }
    
    void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
          ApplyRotation(-rotationThrust);
        }

    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   //freezing rotation to manualy rotate 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  //unfreezing rotation so physics system can take over 
    }
}
