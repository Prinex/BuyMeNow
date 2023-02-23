using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

namespace BuyMeNow.Helpers;

/**
 * use this for the db services
 */
public static class Constants
{
    // name of the local db
    public const string LocalDbFile = "app.db";

    // flags
    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), LocalDbFile);

    // method for importing an allready filled db
    public static void ImportFilledDB()
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
        using (Stream stream = assembly.GetManifestResourceStream($"BuyMeNow.{LocalDbFile}"))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                File.WriteAllBytes(DatabasePath, memoryStream.ToArray());
            }
        }
    }
}

