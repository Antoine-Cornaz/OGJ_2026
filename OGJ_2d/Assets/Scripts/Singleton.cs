using UnityEngine;

namespace Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            var self = this as T;
            if (self == null)
            {
                Debug.LogError($"{GetType().Name} is not a {typeof(T).Name}", this);
                enabled = false;
                return;
            }

            if (Instance != null && Instance != self)
            {
                Destroy(gameObject);
                return;
            }

            Instance = self;
            //DontDestroyOnLoad(gameObject);
        }
    }
}