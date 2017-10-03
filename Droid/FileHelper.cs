using System;
using System.IO;
using LocalDatabaseSample;
using LocalDatabaseSample.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace LocalDatabaseSample.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
