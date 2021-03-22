using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    //PARAMETERS - for tuning, typically set in the editor;
    //CACHE - e.g. references for readability or speed;
    //STATE - private instance (member) variable;

    [SerializeField]  AudioClip mainEngine;
    [SerializeField] float thrust = 100;
    [SerializeField] float rotationThrust;
    
    Rigidbody rb;
    AudioSource audioSource;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
       
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
            
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
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
