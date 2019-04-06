using UnityEngine;
using UnityEngine.Events;

namespace CheckpointSystem
{
	public class Checkpoint : MonoBehaviour
	{

		[SerializeField] private UnityEvent m_onReached;
		[SerializeField] private CheckpointId m_id;
		internal CheckpointId Id => m_id;

		private void Awake()
		{
			CheckpointController.Register(this);
		}

		public void ReachedCheckpoint()
		{
			CheckpointController.Reach(m_id);
		}

		internal void OnReached()
		{
			m_onReached?.Invoke();
		}
	}
}