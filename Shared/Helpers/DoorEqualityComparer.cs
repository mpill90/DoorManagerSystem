using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Shared.Helpers
{
    public class DoorEqualityComparer : IEqualityComparer<Door>
    {
        public bool Equals(Door x, Door y)
        {
            return x.Id == y.Id &&
                    x.Label == y.Label &&
                    x.IsClosed == y.IsClosed &&
                    x.IsLocked == y.IsLocked;
        }

        public int GetHashCode([DisallowNull] Door obj)
        {
            throw new NotImplementedException();
        }
    }
}
