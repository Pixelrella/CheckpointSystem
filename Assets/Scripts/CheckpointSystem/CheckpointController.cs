using System.Collections.Generic;

namespace CheckpointSystem
{
    internal static class CheckpointController
    {

        private static readonly Dictionary<CheckpointId, List<Checkpoint>> CheckpointsInScene = 
            new Dictionary<CheckpointId, List<Checkpoint>>();

        public static void Register(Checkpoint checkpoint)
        {
            if (IsFirstWithId(checkpoint.Id))
            {
                CheckpointsInScene.Add(checkpoint.Id, new List<Checkpoint>());
            }
		
            CheckpointsInScene[checkpoint.Id].Add(checkpoint);
        }

        private static bool IsFirstWithId(CheckpointId id)
        {
            return !CheckpointsInScene.ContainsKey(id);
        }

        public static void Reach(CheckpointId id)
        {
            foreach (var checkpoint in CheckpointsInScene[id])
            {
                checkpoint.OnReached();
            }
		
            //TODO: make persistent
            //TODO: was triggered before
        }
    }
}