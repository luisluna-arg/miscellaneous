private static String GetDbProviders()
{
    // Retrieve the installed providers and factories.
    DataTable table = DbProviderFactories.GetFactoryClasses();

    StringBuilder bob = new StringBuilder();

    // Display each row and column value.
    foreach (DataRow row in table.Rows)
    {
        foreach (DataColumn column in table.Columns)
        {
            bob.AppendLine(" " + row[column] + " ");
        }
    }

    return bob.ToString();
}
