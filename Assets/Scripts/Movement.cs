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
    
    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem mainBoosterParticles;
    
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

    
    void ProcessThrust() //poruszanie
    {
        if (Input.GetKey(KeyCode.Space))
        {

           Thrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    void ProcessRotation()  //skręcanie
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }


        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotation();
        }

    }
    

    void Thrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBoosterParticles.isPlaying)

        {
            mainBoosterParticles.Play();

        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();


    }
    
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   //freezing rotation to manualy rotate 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  //unfreezing rotation so physics system can take over 
    }
    
    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightBoosterParticles.isPlaying)

        {
            rightBoosterParticles.Play();
        }
    }
    
    
    
    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftBoosterParticles.isPlaying)

        {
            leftBoosterParticles.Play();
        }
    }
    
    
    private void StopRotation()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }
    


}
