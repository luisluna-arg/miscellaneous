public void ExportarZipFile()
{
    FileInfo[] files = MethodThatResurnsFileInfoArray();

	/* Tempo directory for files */
    string fileBasePath = files.First().Directory.Parent.FullName + "\\Temp\\";
    if (!Directory.Exists(fileBasePath)) Directory.CreateDirectory(fileBasePath);

    string fileName = @"ExportedZipFile_" + DateTime.Now.ToString("yy.MM.dd.HH.mm.ss") + ".zip";
    string zipFileName = fileBasePath + fileName;

    MemoryStream gZipStream = new MemoryStream();
    using (var zipFile = new ZipArchive(gZipStream, ZipArchiveMode.Create, true))
    {
        for (int i = 0; i < files.Length; i++)
        {
            zipFile.CreateEntryFromFile(files[i].FullName, files[i].Name);
        }
    }

    byte[] fileBytes = gZipStream.ToArray();

    Response.Clear();
    Response.BufferOutput = false;
    Response.ContentType = "application/zip";
    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);

    Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);

    Response.End();
}
