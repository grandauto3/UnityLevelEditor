using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImportExport
{
    public static Texture2D Import()
    {
        string path = EditorUtility.OpenFilePanelWithFilters("Load Level", "", new string[] { "Image files", "png" });

        byte[] texBytes = System.IO.File.ReadAllBytes(path);

        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(texBytes);

        tex.filterMode = FilterMode.Point;

        return tex;
    }

    public static bool Export(Texture2D tex)
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Level", "Level_01", "png", "Save Level");

        if(path == null)
            return false;

        byte[] texBytes = tex.EncodeToPNG();

        if (texBytes == null)
            return false;

        System.IO.File.WriteAllBytes(path, texBytes);
        AssetDatabase.Refresh();

        return true;
    }

}
