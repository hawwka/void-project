using UnityEditor;
using UnityEngine;

public static class ImportHotReloadPackage
{
    private const string PackagePath = "Assets/ThirdParty\\Hot Reload Edit Code Without Compiling v1.12.9";

    [MenuItem("MyTools/Libraries/Import Hot Reload")]
    public static void ImportHotReload()
    {
        if (!System.IO.File.Exists(PackagePath))
        {
            Debug.LogError($"Hot Reload package not found at path: {PackagePath}");
            return;
        }

        AssetDatabase.ImportPackage(PackagePath, false);
        Debug.Log($"Successfully imported Hot Reload package from: {PackagePath}");
    }

    [MenuItem("HCTools/Libraries/Import Hot Reload", true)]
    public static bool ValidateImportHotReload()
    {
        return System.IO.File.Exists(PackagePath);
    }
}