/*
* (c) 2021 VirtaMed AG - Strictly Confidential - All rights reserved
*
* This document contains unpublished, confidential and proprietary information
* of VirtaMed AG. No disclosure or use of any portion of the contents of these
* materials may be made without the express consent of VirtaMed AG.
*/

using UnityEditor;
using UnityEngine;
using VirtaMed.Unity.Common;

namespace VirtaMed.Unity.EditorExtensions.CustomInspectors
{
    [CustomEditor(typeof(ComponentsSorterMono))]
    public class ComponentsSorterInspector : Editor
    {
        private ComponentsSorterMono myClass;

        public override void OnInspectorGUI()
        {
            
            GUILayout.Label ("This will do two things:\n\n1) Separate vanilla Unity from Virtamed component.\n2) The Virtamed component will be sorted to always have the same order.");
        
            if(GUILayout.Button("Run It")) 
            {
                SortComponents();
            }
        
            if (UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
            {
                GUI.contentColor = Color.red;
                GUILayout.Label("You must be in Prefab mode to automatically sort");
                GUI.contentColor = Color.white;
            }
            var headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontSize = 18;
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.alignment = TextAnchor.MiddleCenter;
            GUI.contentColor = Color.green;
            GUILayout.Label("=== VirtaMed Components below ===", headerStyle);
            GUI.contentColor = Color.white;
        }

        private void SortComponents()
        {
            if (UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null)
            {
                myClass = target as ComponentsSorterMono;
                myClass.SortComponents();
            }
            else
            {
                Debug.LogError("Sorting only works in Prefab mode");
            }
        }
    }
}
