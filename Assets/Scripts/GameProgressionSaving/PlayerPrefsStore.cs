using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameProgressionSaving
{
    internal class PlayerPrefsStore : IPersistentProgression
    {
        private List<int> m_unlockedCheckpoints;
        
        private const string CheckpointsKeyPrefix = "Checkpoint";
        private const string NumberOfCheckpointsKey = "CheckpointNumber";
        private const int CheckpointIdDefault = -1;
        
        public PlayerPrefsStore()
        {
            LoadReachedCheckpoints();
        }

        private void LoadReachedCheckpoints()
        {
            m_unlockedCheckpoints = new List<int>();

            var numberOfReachedCheckpoints = PlayerPrefs.GetInt(NumberOfCheckpointsKey, 0);
            for (var checkpointId = 0; checkpointId < numberOfReachedCheckpoints; checkpointId++)
            {
                var checkpointIdSaved = PlayerPrefs.GetInt(CheckpointIdToString(checkpointId), CheckpointIdDefault);
                Assert.IsFalse(checkpointIdSaved == CheckpointIdDefault, "Saved checkpoints out of scope");
                m_unlockedCheckpoints.Add(checkpointId);
            }
        }

        public void PersistCheckpointReached(int id)
        {
            m_unlockedCheckpoints.Add(id);
        }

        public void PersistAll()
        {
            PlayerPrefs.SetInt(NumberOfCheckpointsKey, m_unlockedCheckpoints.Count);
            
            foreach (var unlockedCheckpoint in m_unlockedCheckpoints)
            {
                PlayerPrefs.SetInt(CheckpointIdToString(unlockedCheckpoint), 1);
            }
        }

        private static string CheckpointIdToString(int unlockedCheckpoint)
        {
            return $"{CheckpointsKeyPrefix}{unlockedCheckpoint}";
        }

        public IEnumerable<int> GetReachedCheckpoints()
        {
            return m_unlockedCheckpoints;
        }
    }
}