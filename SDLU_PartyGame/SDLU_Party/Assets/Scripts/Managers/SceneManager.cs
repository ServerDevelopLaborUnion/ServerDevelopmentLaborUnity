using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneManager : MonoSingleton<SceneManager>
    {
        [Header("로드할 Scene 이름")]
        public List<string> _sceneName = new List<string>();
        [Header("로드할 Scene 파일")]
        public List<Scene> _sceneList = new List<Scene>();

        public event System.Action OnSceneLoad;

        private void Awake()
        {
            OnSceneLoad += () => { };
            // CheckList();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.ArgumentException"></exception>
        private void CheckList()
        {
            if(_sceneName.Count != _sceneList.Count) throw new System.ArgumentException("_sceneName and _sceneList has different length");
        }

        public void LoadScene(string name)
        {
            // if(!_sceneName.Contains(name)) throw new System.ArgumentNullException($"Cannot find request scenename \"{name}\" in {_sceneName.GetType()} _sceneName");
            OnSceneLoad();
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        }



    }
}
