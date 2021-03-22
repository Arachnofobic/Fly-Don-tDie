using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDealay = 2;
    [SerializeField] AudioClip finish;
    [SerializeField]  AudioClip crash;
    
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;


    bool isTransitioning = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
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
