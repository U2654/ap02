using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    void Awake()
    {
        if (Object.FindFirstObjectByType<PersistentObject>() != null && Object.FindFirstObjectByType<PersistentObject>() != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }

        DontDestroyOnLoad(gameObject); // Keep this object alive across scene loads
    }
}
