using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1.5f;

    void OnCollisionEnter(Collision collision)
    {
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
        // todo: add SFX upon crash
        // todo: add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void StartFinishSequence()
    {
        // todo: add SFX upon finish
        // todo: add particle effect upon finish
        GetComponent<Movement>().enabled = true;
        Invoke("LoadNextLevel", loadDelay); // better to use a coroutine instead later on
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
