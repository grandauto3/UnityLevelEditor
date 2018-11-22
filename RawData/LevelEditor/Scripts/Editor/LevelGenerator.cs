using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelGenerator
{
    static Texture2D map;
    static GameObject levelParent;

    public static void GenerateLevel(Texture2D map, bool is3D)
    {
        LevelGenerator.map = map;

        if (GameObject.Find("Level"))
            Object.DestroyImmediate(GameObject.Find("Level"));

        if (!levelParent)
            levelParent = new GameObject("Level");

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y, is3D);


            }
        }

        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
    }

    static void GenerateTile(int x, int y, bool is3D)
    {
        Color32 pixelColor = map.GetPixel(x, y);

        Color32 col = new Color32(205, 205, 205, 205);

        if (pixelColor == Color.clear || pixelColor == (Color)col)
            return;

        if(MainWindow.toolwnd.tiles == null)
        {
            Debug.LogWarning("Tileslist is empty");
            return;
        }

        for (int i = 0; i < MainWindow.toolwnd.tiles.Length; i++)
        {
            if (MainWindow.toolwnd.tiles[i].color == (Color)pixelColor)
                
                if (MainWindow.toolwnd.tiles[i].prefab)
                    if (is3D)
                        Object.Instantiate(MainWindow.toolwnd.tiles[i].prefab,
                            new Vector3(x + MainWindow.toolwnd.tiles[i].prefab.transform.localScale.x, 0, y + MainWindow.toolwnd.tiles[i].prefab.transform.localScale.z),
                            Quaternion.identity, levelParent.transform);
                    else
                        Object.Instantiate(MainWindow.toolwnd.tiles[i].prefab,
                            new Vector2(x + MainWindow.toolwnd.tiles[i].prefab.transform.localScale.x, y + MainWindow.toolwnd.tiles[i].prefab.transform.localScale.y),
                            Quaternion.identity, levelParent.transform);
                else
                    Debug.LogWarning("A Prefab is missing");
            
        }
    }
}
