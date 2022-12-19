using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityExt
{
    public abstract class PersistentSingletonBehaviourProtected<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance = null;
        bool validRef = false;
        protected static T instance
        {
            get
            {
                return _instance;
            }
        }
        void Awake()
        {
            validRef = false;
            if (_instance == null)
            {
                _instance = this as T;
                OnFirstLoad();
                SceneManager.sceneLoaded += OnSceneLoaded;
                DontDestroyOnLoad(gameObject);
                validRef = true;
            }
            else if (_instance != this as T)
            {
                DestroyImmediate(gameObject);
            }
        }
        void OnDestroy()
        {
            if (validRef)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }
        IEnumerator Start()
        {
            if (validRef)
            {
                yield return null;
                StartCoroutine(OnAwakeBehaviourAsync());
            }
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0 || scene.name == "boot")
            {
                OnBootSceneLoaded();
            }
            else
            {
                OnGameSceneLoaded(scene);
            }
            OnAnySceneLoaded(scene);
        }
        protected virtual void OnBootSceneLoaded() { }
        protected virtual void OnAnySceneLoaded(Scene scene) { }
        protected virtual void OnGameSceneLoaded(Scene gameScene) { }
        protected virtual IEnumerator OnAwakeBehaviourAsync() { yield return null; }
        protected virtual void OnFirstLoad() { }
    }
}