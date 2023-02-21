using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1.5f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugCheatKeys();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Seems like it is a friendly object!");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            /*case "Fuel":
                Debug.Log("You've picked up some fuel, nice!");
                break; */
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX, 0.7f);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);

        // todo: add particle effect upon crash       
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        // todo: add particle effect upon finish
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadDelay); // better to use a coroutine instead later on

    }
    void DebugCheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Cheat activated - disabling collisions!");
            collisionDisabled = !collisionDisabled;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Cheat activated - loading next level!");
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("A finish! Good job!");
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("You've probably collided with a not very friendly object. You die, hehe.");
    }

}
