using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[InitializeOnLoad]
public static class TimelineWindowExtension
{
    static TimelineWindowExtension()
    {
        // エディタの更新時に呼び出す
        EditorApplication.update += OnEditorUpdate;
    }

    static void OnEditorUpdate()
    {
        var timelineWindowType = Type.GetType("UnityEditor.Timeline.TimelineWindow, Unity.Timeline.Editor");
        if (timelineWindowType == null) return;

        var timelineWindows = Resources.FindObjectsOfTypeAll(timelineWindowType);
        if (timelineWindows.Length <= 0)
        {
            return;
        }

        var timelineWindow = timelineWindows[0] as EditorWindow;

        if (timelineWindow != null)
        {
            var root = timelineWindow.rootVisualElement;
            if (root.Q<Button>("グループの更新") == null)
            {
                var button = new Button(FireGroupEnumGenerator.GenerateFireGroupEnum)
                {
                    text = "グループの更新",
                    name = "グループの更新"
                };

                root.Add(button);
            }
        }
    }
}
