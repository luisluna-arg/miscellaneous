/// <summary>
/// Extension method: Returns the moment the application compilation has ended as a DateTime
/// </summary>
/// <param name="assembly">The the application assembly related class</param>
/// <param name="targetTZ">Optional timezone to consider</param>
/// <returns></returns>
public static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo targetTZ = null)
{
    /* App generated DLL location */
    string filePath = assembly.Location;

    /* DLL PE Header Offset */
    const int PE_HEADER_OFFSET = 60;

    const int LINKER_TIMESTAMP_OFFSET = 8;

    /* Read generated DLL bytestream */
    byte[] buffer = new byte[2048];
    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        stream.Read(buffer, 0, 2048);

    /* From the file header obtain the generation date as seconds since 1970 */
    int offset = BitConverter.ToInt32(buffer, PE_HEADER_OFFSET);
    /* How many seconds since 1970 */
    int secondsSince1970 = BitConverter.ToInt32(buffer, offset + LINKER_TIMESTAMP_OFFSET);
    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /* Find the file generation date */
    DateTime linkTimeUtc = epoch.AddSeconds(secondsSince1970);

    /* Return compilation time, according to local or provided timezone */
    return TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, targetTZ ?? TimeZoneInfo.Local);
}
