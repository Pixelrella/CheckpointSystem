using CheckpointSystem;
using GameProgressionSaving;
using UnityEngine;

namespace DefaultNamespace
{
    public class Main : MonoBehaviour
    {
        private PlayerPrefsStore m_persistentProgression;

        private void Awake()
        {
            m_persistentProgression = new PlayerPrefsStore();
            
            CheckpointController.Initialise(m_persistentProgression);
        }

        private void OnDestroy()
        {
            m_persistentProgression.PersistAll();
        }
    }
}