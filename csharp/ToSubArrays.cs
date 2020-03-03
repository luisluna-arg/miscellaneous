public static T[][] ToSubArrays<T>(this IEnumerable<T> items, int subsetSize)
{
    T[][] chunks = items
            .Select((s, i) => new { Value = s, Index = i })
            .GroupBy(x => x.Index / 1000)
            .Select(grp => grp.Select(x => x.Value).ToArray())
            .ToArray();

    return chunks;
}
