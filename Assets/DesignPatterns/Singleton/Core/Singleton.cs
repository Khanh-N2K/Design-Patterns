using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    [Header("=== SINGLETON ===")]

    [Header("Data")]
    public static T Instance { get; private set; }

    public virtual void Initialize()
    {
        if (Instance != null)
        {
            Debug.LogError($"There's more than 1 instance of {typeof(T)} existed!");
            return;
        }

        Instance = (T)this;
    }
}
