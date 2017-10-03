/*Dependency Service to find the file path within the Native Device
 */

namespace LocalDatabaseSample
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
