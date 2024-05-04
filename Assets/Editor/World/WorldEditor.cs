using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor for the WorldGenerator class.
/// </summary>
[CustomEditor(typeof(WorldGenerator))]
public class WorldEditor : Editor
{
    WorldGenerator generator;  // The WorldGenerator instance
    Editor shapeEditor;  // Editor for the shape settings
    Editor colorEditor;  // Editor for the color settings

    // Foldout states for the shape and color settings
    bool shapeSettingsFolded;
    bool colorSettingsFolded;

    bool autoUpdate;  // Flag to determine if the world should auto-update when settings change

    /// <summary>
    /// Override the OnInspectorGUI method to customize the inspector.
    /// </summary>
    public override void OnInspectorGUI()
    {
        // Check if any changes are made in the inspector
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            // If changes are made, regenerate the world
            if (check.changed)
            {
                generator.GenerateWorld();
            }
        }

        // Add a button to manually generate the world
        if (GUILayout.Button("Generate Planet"))
        {
            generator.GenerateWorld();
        }

        // Add a toggle for auto-update
        autoUpdate = GUILayout.Toggle(autoUpdate, "Auto Update");

        // Draw the editors for the shape and color settings
        // If auto-update is enabled, pass the update method, otherwise pass null
        DrawSettingsEditor(generator.shapeSettings, autoUpdate ? generator.OnShapeSettingsUpdate : null, ref shapeSettingsFolded, ref shapeEditor);
        DrawSettingsEditor(generator.colorSettings, autoUpdate ? generator.OnShapeColorUpdate : null, ref colorSettingsFolded, ref colorEditor);
    }

    /// <summary>
    /// Draws the settings editor.
    /// </summary>
    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool folded, ref Editor editor)
    {
        // If settings is null, do nothing
        if (settings == null)
        {
            return;
        }

        // Draw the inspector title bar and get the foldout state
        folded = EditorGUILayout.InspectorTitlebar(folded, settings);
        // Check if any changes are made in the settings editor
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            // If the settings are folded, do nothing
            if (!folded)
            {
                return;
            }

            // Create an editor for the settings and draw it
            CreateCachedEditor(settings, null, ref editor);
            editor.OnInspectorGUI();

            // If changes are made, call the update method
            if (check.changed)
            {
                if (onSettingsUpdated != null)
                {
                    onSettingsUpdated();
                }
            }
        }
    }

    /// <summary>
    /// Called when the editor is enabled.
    /// </summary>
    private void OnEnable()
    {
        // Get the WorldGenerator instance
        generator = (WorldGenerator)target;
    }
}