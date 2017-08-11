// -----------------------------------------------------------------------
// <copyright file="UpdateValues.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace IntergalacticToRoman
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UpdateValues<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public void AddUpdateKey(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                Add(key, value);
            }
        }
    }
}
