using UnityEngine;

public class CollisionHandler : MonoBehaviour
{


    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Seems like it is a friendly object!");
                break;
            case "Finish":
                Debug.Log("A finish! Good job!");
                break;
            case "Fuel":
                Debug.Log("You've picked up some fuel, nice!");
                break;
            default:
                Debug.Log("You've probably collided with a not very friendly object. You die, hehe.");
                break;
        }


    }
}
