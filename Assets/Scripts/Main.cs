using CheckpointSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class Main : MonoBehaviour
    {
        private void Awake()
        {
            CheckpointController.Initialise();
        }
    }
}