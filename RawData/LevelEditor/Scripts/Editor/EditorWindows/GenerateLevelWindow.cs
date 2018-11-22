using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenerateLevelWindow : EditorWindow
{
    static GenerateLevelWindow instance;

    public static GenerateLevelWindow Init()
    {
        instance = GetWindow<GenerateLevelWindow>();

        return instance;
    }

    void OnGUI()
    {
        GUIStyle style = EditorStyles.boldLabel;
        style.alignment = TextAnchor.MiddleCenter;

        GUILayout.Label("Generate Level", style);

        if(MainWindow.lvl == null)
        {
            GUILayout.Label("No Level in Editor", style);
            return;
        }


        if (GUILayout.Button("Generate 2D Level"))
        {
            LevelGenerator.GenerateLevel(MainWindow.lvl.Tex, false);
            instance.Close();
        }
        if(GUILayout.Button("Generate 3D Level"))
        {
            LevelGenerator.GenerateLevel(MainWindow.lvl.Tex, true);
            instance.Close();
        }
    }
}
