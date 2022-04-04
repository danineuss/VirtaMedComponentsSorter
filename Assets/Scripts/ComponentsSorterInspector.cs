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
        private ComponentsSorterMono _componentSorterMono;

        public override void OnInspectorGUI()
        {
            GUILayout.Label ("This will do two things:\n\n" +
                "1) Separate vanilla Unity from Virtamed component.\n" +
                "2) The Virtamed component will be sorted to always have the same order.");
        
            if(GUILayout.Button("Run It")) 
                SortComponents();
        
            if (UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
            {
                GUI.contentColor = Color.red;
                GUILayout.Label("You must be in Prefab mode to automatically sort");
                GUI.contentColor = Color.white;
            }
            var headerStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            GUI.contentColor = Color.green;
            GUILayout.Label("=== VirtaMed Components below ===", headerStyle);
            GUI.contentColor = Color.white;
        }

        private void SortComponents()
        {
            if (UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
            {
                Debug.LogError("Sorting only works in Prefab mode");
                return;
            }

            _componentSorterMono = target as ComponentsSorterMono;
            _componentSorterMono.SortComponents();
        }
    }
}
