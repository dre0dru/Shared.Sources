using UnityEngine;

namespace Shared.Sources.UDatabase
{
    public static class UDatabaseExtensions
    {
        public static UDatabaseSo<TKey, TValue> Clone<TKey, TValue, TKvp>(this UDatabaseSo<TKey, TValue> uDatabaseSo)
        {
            return Object.Instantiate(uDatabaseSo);
        }
    }
}
