﻿using UnityEngine;
using System.Collections;

using System;
using UnityEditor;


[Flags]
public enum EditorListOption
{
    None = 0,
    ListSize = 1,
    ListLabel = 2,
    ElementLabels = 4,
    Buttons = 8,
    Default = ListSize | ListLabel | ElementLabels,
    NoElementLabels = ListSize | ListLabel,
    All = Default | Buttons
}

public static class EditorList
{
    private static GUIContent
        moveButtonContent = new GUIContent("\u21b4", "move down"),
        duplicateButtonContent = new GUIContent("+", "duplicate"),
        deleteButtonContent = new GUIContent("-", "delete"),
        addButtonContent = new GUIContent("+", "add element");

    public static void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default)
    {
        bool
            showListLabel = (options & EditorListOption.ListLabel) != 0,
            showListSize = (options & EditorListOption.ListSize) != 0;

        if (showListLabel)
        {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
        }
        if (!showListLabel || list.isExpanded)
        {
            if (showListSize)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            }
            ShowElements(list, options);
        }
        if (showListLabel)
        {
            EditorGUI.indentLevel -= 1;
        }
    }


    private static void ShowElements(SerializedProperty list, EditorListOption options)
    {        
        bool
            showElementLabels = (options & EditorListOption.ElementLabels) != 0,
            showButtons = (options & EditorListOption.Buttons) != 0;

        for (int i = 0; i < list.arraySize; i++)
        {
            if (showButtons)
            {
                EditorGUILayout.BeginHorizontal();
            }
            if (showElementLabels)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
            else
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
            }
            if (showButtons)
            {
                ShowButtons(list,i);
                EditorGUILayout.EndHorizontal();
             
            }
        }
        if (showButtons && list.arraySize == 0 && GUILayout.Button(addButtonContent, EditorStyles.miniButton)) {
			list.arraySize += 1;
		}
    }

    private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

    private static void ShowButtons(SerializedProperty list, int index)
    {
        if (GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
        {
            list.MoveArrayElement(index, index + 1);
        }
        if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonMid, miniButtonWidth))
        {
            list.InsertArrayElementAtIndex(index);
        }
        if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth))
        {
            list.DeleteArrayElementAtIndex(index);
        }
    }

}