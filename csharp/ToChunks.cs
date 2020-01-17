/* This method splits a collection into groups of N elements */

public static IEnumerable<IEnumerable<T>> ToChunks<T>(IEnumerable<T> source, int n)
{
    return  source
        .Select((x, i) => new { Index = i, Value = x })
        .GroupBy(x => x.Index / n)
        .Select(x => x.Select(v => v.Value));
}
