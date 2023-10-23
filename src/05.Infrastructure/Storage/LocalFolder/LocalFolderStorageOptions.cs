﻿namespace Pertamina.Website_KPI.Infrastructure.Storage.LocalFolder;

public class LocalFolderStorageOptions
{
    public static readonly string SectionKey = $"{nameof(Storage)}:{nameof(LocalFolder)}";

    public string FolderPath { get; set; } = default!;
}
