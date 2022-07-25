
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField] AudioClip WinSound;
    [SerializeField] AudioClip LoseSound;


    [SerializeField] ParticleSystem WinParticles;
    [SerializeField] ParticleSystem LoseParticles;

    AudioSource audioHandler;

    bool isTransitioning = false;
    bool isColliding = false;


    void Start()
    {
        audioHandler = GetComponent<AudioSource>();
    }


    void Update()
    {
        RespondToDebug();
    }

    private void RespondToDebug()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            isColliding = !isColliding;
        }
    }

    void OnCollisionEnter(Collision other)
    {

        if (isTransitioning || isColliding) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It is friendly");
                break;
            case "Finish":
                WinParticles.Play();
                StartSuccessSequence();
                break;

            default:
                // Debug.Log("Destroy the rocket");
                isTransitioning = true;
                StartCrashSequence();
                break;

        }

    }






    void StartCrashSequence()
    {
        isTransitioning = true;
        audioHandler.Stop();
        LoseParticles.Play();

        audioHandler.PlayOneShot(LoseSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioHandler.Stop();
        audioHandler.PlayOneShot(WinSound);
        GetComponent<Movement>().enabled = false;

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }



}
