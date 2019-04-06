using UnityEngine;
using UnityEngine.Events;

namespace UsefulMonos
{
    public class UnityEventTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_eventToTrigger;

        private void OnTriggerEnter(Collider other)
        {
            m_eventToTrigger?.Invoke();
        }
    }
}