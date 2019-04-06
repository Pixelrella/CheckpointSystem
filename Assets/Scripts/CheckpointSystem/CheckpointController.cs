using System.Collections.Generic;
using System.Linq;
using GameProgressionSaving;
using UnityEngine;

namespace CheckpointSystem
{
    internal static class CheckpointController
    {
        private static Dictionary<CheckpointId, List<Checkpoint>> checkpointsInScene;
        private static IPersistentProgression persistentProgression;
        private static List<CheckpointId> reachedCheckpointIds;

        public static void Initialise(IPersistentProgression persistentProgressionIn)
        {
            checkpointsInScene = new Dictionary<CheckpointId, List<Checkpoint>>();
            
            persistentProgression = persistentProgressionIn;

            reachedCheckpointIds = persistentProgression.GetReachedCheckpoints()
                                                        .Cast<CheckpointId>()
                                                        .ToList();
        }

        public static void Register(Checkpoint checkpoint)
        {
            var id = checkpoint.Id;
            
            if (IsFirstWithId(id))
            {
                checkpointsInScene.Add(id, new List<Checkpoint>());
            }
		
            checkpointsInScene[id].Add(checkpoint);

            if (IsReached(id))
            {
                checkpoint.OnReached();
            }
        }

        private static bool IsReached(CheckpointId id)
        {
            return reachedCheckpointIds.Contains(id);
        }

        private static bool IsFirstWithId(CheckpointId id)
        {
            return !checkpointsInScene.ContainsKey(id);
        }

        public static void Reach(CheckpointId id)
        {
            if (IsReached(id))
            {
                Debug.LogWarning($"Tried to reach already reached Checkpoint {id}");
                return;
            }
            
            InformAllCheckpointsWithId(id);

            reachedCheckpointIds.Add(id);
            persistentProgression.PersistCheckpointReached((int)id);
        }

        private static void InformAllCheckpointsWithId(CheckpointId id)
        {
            foreach (var checkpoint in checkpointsInScene[id])
            {
                checkpoint.OnReached();
            }
        }
    }
}