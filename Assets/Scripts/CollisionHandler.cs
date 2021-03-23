using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDealay = 2;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem crashParticles;


    AudioSource audioSource;


    bool isTransitioning = false;
    bool collisionDisabled = false;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }


    void RespondToDebugKeys()  //cheat keys;
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();


        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning|| collisionDisabled)

{
            return;
        }

        
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                
                case "Finish":
                    StartSuccesSequence();
                    break;
                
                case "Fuel":
                    Debug.Log("You've picked up fuel");
                    break;
                
                default:
                    Debug.Log("Welp. You blew up :/");
                    StartCrashSequence();
                    break;
            }
    }



    void StartSuccesSequence()
    {
        isTransitioning = true;
        
        audioSource.Stop();
        audioSource.PlayOneShot(finish);
        finishParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke ("LoadNextLevel",levelLoadDealay); 
        
    }
    
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel",levelLoadDealay);
        
    }
    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+ 1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {

            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    
}
