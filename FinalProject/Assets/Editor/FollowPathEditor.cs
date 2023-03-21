/*
 * This file creates a subsection on the FollowPath Script component editor in the inspector.
 * Specifically it allows to you define a set number of waypoints and the location for each.
 * Additionally it creates handles for each of the points making it easy to adjust the path.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

// Create custom editor for FollowPath script
[CustomEditor(typeof(FollowPath))]

// Note that the class inherits from Editor, not MonoBehavior
public class FollowPathEditor : Editor
{
    // The component this editor will exist for
    private FollowPath targetComponent;

    // This function allows the editor to modify things in the scene view
    // Note that while this is normal behavior for a MonoBehavior class (game object),
    // a component editor does not always need to display something to the scene
    private void OnSceneGUI()
    {
        // target is inherent to the Editor class and is of type Object, the base class for all game objects
        // Here we typecast it to FollowPath so that we can access its specific properties
        targetComponent = (FollowPath) target;

        // Setting the color for the handles we will later generate
        Handles.color = Color.cyan;

        // copying the array of patrol positions
        var positions = targetComponent.path;

        // Drawing a line between all patrol positions to visualize the path the unit will take
        for(int i = 1; i < positions.Length + 1; i++)
        {
            var previousPoint = positions[i - 1];
            var currentPoint = positions[i % positions.Length];

            Handles.DrawDottedLine(previousPoint, currentPoint, 4f);
        }

        // Reusing the copied patrol position array to contain the set of handles which we use to adjust the path
        for(int i = 0; i  < positions.Length; i++)
        {
            positions[i] = Handles.PositionHandle(positions[i], Quaternion.identity);
        }

        // Checks if we modified the GUI and if so notifies the scene that it has been changed
        // This is why you see the "*" next to the file name when you have unsaved changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(targetComponent);
            EditorSceneManager.MarkSceneDirty(targetComponent.gameObject.scene);
        }
    }
}
