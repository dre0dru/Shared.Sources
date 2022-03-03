namespace Shared.Sources.Stats
{
    public static class StatsExtensions
    {
        public static Stat<TKey, TValue> Override<TKey, TValue>(this Stat<TKey, TValue> target, Stat<TKey, TValue> source)
        {
            foreach (var kvp in source)
            {
                target[kvp.Key] = kvp.Value;
            }

            return target;
        }

        public static StatsCollection<TKey, TValue> Override<TKey, TValue>(this StatsCollection<TKey, TValue> target,
            StatsCollection<TKey, TValue> source)
        {
            foreach (var kvp in source)
            {
                if (!target.TryGet(kvp.Key, out var stat))
                {
                    stat = new Stat<TKey, TValue>();
                    target.Add(kvp.Key, stat);
                }

                target[kvp.Key] = stat.Override(kvp.Value);
            }

            return target;
        }

        public static Stat<TKey, TValue> Clone<TKey, TValue>(this Stat<TKey, TValue> source)
        {
            var stat = new Stat<TKey, TValue>();

            stat.Override(source);

            return stat;
        }

        public static StatsCollection<TKey, TValue> Clone<TKey, TValue>(this StatsCollection<TKey, TValue> source)
        {
            var statsCollection = new StatsCollection<TKey, TValue>();

            statsCollection.Override(source);

            return statsCollection;
        }
        
                
        public static FlagsCollection<TKey> Override<TKey>(this FlagsCollection<TKey> target, FlagsCollection<TKey> source)
        {
            foreach (var kvp in source)
            {
                if (!target.TryGet(kvp.Key, out var flag))
                {
                    flag = new Flag();
                    target.Add(kvp.Key, flag);
                }

                target[kvp.Key] = flag.Override(kvp.Value);
            }

            return target;
        }
    }
}
