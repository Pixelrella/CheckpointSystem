using System.Collections.Generic;

namespace GameProgressionSaving
{
    public interface IPersistentProgression
    {
        void PersistCheckpointReached(int id);
        IEnumerable<int> GetReachedCheckpoints();
    }
}