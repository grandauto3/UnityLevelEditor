using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateNewLevelWindow : EditorWindow
{
    static CreateNewLevelWindow instance;

    int x = 16;
    int y = 16;

    public static CreateNewLevelWindow Init()
    {
        instance = GetWindow<CreateNewLevelWindow>(true) as CreateNewLevelWindow;
        instance.maxSize = new Vector2(256, 128);
        instance.minSize = instance.maxSize;

        instance.ShowPopup();

        return instance;
    }

    void OnGUI()
    {
        GUILayout.Label("Create mew Level", EditorStyles.boldLabel);

        x = Mathf.Clamp(EditorGUILayout.IntField("Width: ", x), 1, 256);
        y = Mathf.Clamp(EditorGUILayout.IntField("Height: ", y), 1, 256);

        EditorGUILayout.Space();

        if (GUILayout.Button("Create"))
        {
            Close();

            MainWindow.lvl = new Level(x, y);
        }
    }
}
