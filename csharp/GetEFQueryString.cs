/* 
Code extracted from https://www.stevefenton.co.uk/2015/07/getting-the-sql-query-from-an-entity-framework-iqueryable/ 
Here is his Github profile: https://github.com/Steve-Fenton/
*/
private static ObjectQuery<T> GetQueryFromQueryable<T>(IQueryable<T> query)
{
  var internalQueryField = query.GetType().GetFields(
      System.Reflection.BindingFlags.NonPublic | 
      System.Reflection.BindingFlags.Instance
      ).Where(f => f.Name.Equals("_internalQuery")).FirstOrDefault();
  var internalQuery = internalQueryField.GetValue(query);
  var objectQueryField = internalQuery.GetType().GetFields(
      System.Reflection.BindingFlags.NonPublic | 
      System.Reflection.BindingFlags.Instance
      ).Where(f => f.Name.Equals("_objectQuery")).FirstOrDefault();
  return objectQueryField.GetValue(internalQuery) as ObjectQuery<T>;
}
