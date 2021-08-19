// Copyright (C) 2018 Mercuna Developments Limited - All rights reserved
// This source file is part of the Mercuna Middleware
// Use, modification and distribution of this file or any portion thereof
// is only permitted as specified in your Mercuna License Agreement.
using System;
using System.Runtime.InteropServices;
using UnityEditor;

namespace Mercuna.Editor
{
    [CustomEditor(typeof(Mercuna))]
    public class MercunaEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Mercuna Advanced Settings", EditorStyles.boldLabel);

            bool bExtraLogging = EditorGUILayout.Toggle("Extra logging", MerSettings.extraLogging);
            if (bExtraLogging != MerSettings.extraLogging)
            {
                MerSettings.extraLogging = bExtraLogging;
                Mercuna.SetExtraLogging(bExtraLogging);
            }

            bool bAlwaysShowErrors = EditorGUILayout.Toggle("Always show errors", MerSettings.alwaysShowErrors);
            if (bAlwaysShowErrors != MerSettings.alwaysShowErrors)
            {
                MerSettings.alwaysShowErrors = bAlwaysShowErrors;
                Mercuna.SetAlwaysShowErrors(bAlwaysShowErrors);
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Mercuna Info", EditorStyles.boldLabel);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.LabelField("Version", Mercuna.GetVersion());
            EditorGUILayout.LabelField("Memory Used", String.Format("{0}KB", GetMemInUse() / 1024));
            EditorGUI.EndDisabledGroup();
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern int GetMemInUse();
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern bool HasInstrumentedAlloc();
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void DumpAllocs();
    }
}
