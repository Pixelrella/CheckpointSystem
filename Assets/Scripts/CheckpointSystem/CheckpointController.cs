using System.Collections.Generic;

namespace CheckpointSystem
{
    internal static class CheckpointController
    {
        private static Dictionary<CheckpointId, List<Checkpoint>> checkpointsInScene;

        public static void Initialise()
        {
            checkpointsInScene = new Dictionary<CheckpointId, List<Checkpoint>>();
        }

        public static void Register(Checkpoint checkpoint)
        {
            if (IsFirstWithId(checkpoint.Id))
            {
                checkpointsInScene.Add(checkpoint.Id, new List<Checkpoint>());
            }
		
            checkpointsInScene[checkpoint.Id].Add(checkpoint);
        }

        private static bool IsFirstWithId(CheckpointId id)
        {
            return !checkpointsInScene.ContainsKey(id);
        }

        public static void Reach(CheckpointId id)
        {
            foreach (var checkpoint in checkpointsInScene[id])
            {
                checkpoint.OnReached();
            }
		
            //TODO: make persistent
            //TODO: was triggered before
        }
    }
}