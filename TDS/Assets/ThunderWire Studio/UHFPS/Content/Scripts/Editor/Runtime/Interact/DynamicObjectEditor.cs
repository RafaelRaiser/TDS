using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UHFPS.Runtime;
using ThunderWire.Editors;

namespace UHFPS.Editors
{
    [CustomEditor(typeof(DynamicObject))]
    public class DynamicObjectEditor : Editor
    {
        private DynamicObject _target;

        private SerializedProperty dynamicType;
        private SerializedProperty dynamicStatus;
        private SerializedProperty interactType;
        private SerializedProperty statusChange;

        private SerializedProperty m_target;
        private SerializedProperty audioSource;
        private SerializedProperty animator;
        private SerializedProperty joint;
        private SerializedProperty rigidbody;

        private SerializedProperty unlockScript;
        private SerializedProperty keepUnlockItem;
        private SerializedProperty unlockItem;
        private SerializedProperty showLockedText;
        private SerializedProperty lockedText;

        // ignore colliders
        private SerializedProperty ignoreColliders;
        private SerializedProperty ignorePlayerCollider;
        private ReorderableList ignoreCollidersList;

        // animation triggers
        private SerializedProperty useTrigger1;
        private SerializedProperty useTrigger2;
        private SerializedProperty useTrigger3;

        // dynamic types
        private SerializedProperty openable;
        private PropertyCollection openableProperties;

        private SerializedProperty pullable;
        private PropertyCollection pullableProperties;

        private SerializedProperty switchable;
        private PropertyCollection switchableProperties;

        private SerializedProperty rotable;
        private PropertyCollection rotableProperties;

        // sounds
        private SerializedProperty useSound1;
        private SerializedProperty useSound2;
        private SerializedProperty lockedSound;
        private SerializedProperty unlockSound;

        private SerializedProperty useEvent1;
        private SerializedProperty useEvent2;
        private SerializedProperty onValueChange;
        private SerializedProperty lockedEvent;
        private SerializedProperty unlockedEvent;

        private SerializedProperty lockPlayer;

        private DynamicObject.DynamicType dynamicTypeEnum;
        private DynamicObject.InteractType interactTypeEnum;
        private DynamicObject.DynamicStatus dynamicStatusEnum;
        private DynamicObject.StatusChange statusChangeEnum;

        private void OnEnable()
        {
            _target = target as DynamicObject;

            dynamicType = serializedObject.FindProperty("dynamicType");
            dynamicStatus = serializedObject.FindProperty("dynamicStatus");
            interactType = serializedObject.FindProperty("interactType");
            statusChange = serializedObject.FindProperty("statusChange");

            m_target = serializedObject.FindProperty("target");
            audioSource = serializedObject.FindProperty("audioSource");
            animator = serializedObject.FindProperty("animator");
            joint = serializedObject.FindProperty("joint");
            rigidbody = serializedObject.FindProperty("rigidbody");

            unlockScript = serializedObject.FindProperty("unlockScript");
            keepUnlockItem = serializedObject.FindProperty("keepUnlockItem");
            unlockItem = serializedObject.FindProperty("unlockItem");
            showLockedText = serializedObject.FindProperty("showLockedText");
            lockedText = serializedObject.FindProperty("lockedText");

            ignoreColliders = serializedObject.FindProperty("ignoreColliders");
            ignorePlayerCollider = serializedObject.FindProperty("ignorePlayerCollider");

            ignoreCollidersList = new ReorderableList(serializedObject, ignoreColliders, true, false, true, true);
            ignoreCollidersList.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                SerializedProperty element = ignoreColliders.GetArrayElementAtIndex(index);
                rect.y += EditorGUIUtility.standardVerticalSpacing;
                ReorderableList.defaultBehaviours.DrawElement(rect, element, null, isActive, isFocused, true, true);
            };

            useTrigger1 = serializedObject.FindProperty("useTrigger1");
            useTrigger2 = serializedObject.FindProperty("useTrigger2");
            useTrigger3 = serializedObject.FindProperty("useTrigger3");

            // dynamic types
            {
                openable = serializedObject.FindProperty("openable");
                openableProperties = EditorDrawing.GetAllProperties(openable);

                pullable = serializedObject.FindProperty("pullable");
                pullableProperties = EditorDrawing.GetAllProperties(pullable);

                switchable = serializedObject.FindProperty("switchable");
                switchableProperties = EditorDrawing.GetAllProperties(switchable);

                rotable = serializedObject.FindProperty("rotable");
                rotableProperties = EditorDrawing.GetAllProperties(rotable);
            }

            useSound1 = serializedObject.FindProperty("useSound1");
            useSound2 = serializedObject.FindProperty("useSound2");
            lockedSound = serializedObject.FindProperty("lockedSound");
            unlockSound = serializedObject.FindProperty("unlockSound");

            useEvent1 = serializedObject.FindProperty("useEvent1");
            useEvent2 = serializedObject.FindProperty("useEvent2");
            onValueChange = serializedObject.FindProperty("onValueChange");
            lockedEvent = serializedObject.FindProperty("lockedEvent");
            unlockedEvent = serializedObject.FindProperty("unlockedEvent");

            lockPlayer = serializedObject.FindProperty("lockPlayer");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorDrawing.DrawInspectorHeader(new GUIContent("Dynamic Object"), target);
            EditorGUILayout.Space();

            DrawDynamicTypeGroup();
            EditorGUILayout.Space();
            EditorDrawing.Separator();
            EditorGUILayout.Space();

            dynamicTypeEnum = (DynamicObject.DynamicType)dynamicType.enumValueIndex;
            interactTypeEnum = (DynamicObject.InteractType)interactType.enumValueIndex;
            dynamicStatusEnum = (DynamicObject.DynamicStatus)dynamicStatus.enumValueIndex;
            statusChangeEnum = (DynamicObject.StatusChange)statusChange.enumValueIndex;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(interactType);
                EditorGUILayout.PropertyField(dynamicStatus);

                if (dynamicStatusEnum != DynamicObject.DynamicStatus.Normal)
                    EditorGUILayout.PropertyField(statusChange);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();

            if (dynamicStatusEnum == DynamicObject.DynamicStatus.Locked)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Status Change", EditorStyles.boldLabel);

                if (statusChangeEnum == DynamicObject.StatusChange.InventoryItem)
                {
                    EditorGUILayout.PropertyField(unlockItem, new GUIContent("Unlock Item"));
                    EditorGUILayout.PropertyField(keepUnlockItem);
                }
                else if (statusChangeEnum == DynamicObject.StatusChange.CustomScript)
                {
                    EditorGUILayout.Space(1f);
                    EditorGUILayout.PropertyField(unlockScript);
                }

                EditorGUILayout.PropertyField(showLockedText);
                if (showLockedText.boolValue)
                {
                    EditorGUILayout.PropertyField(lockedText);
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("References", EditorStyles.boldLabel);

            switch (dynamicTypeEnum)
            {
                case DynamicObject.DynamicType.Openable:
                    DrawOpenableProperties();
                    break;
                case DynamicObject.DynamicType.Pullable:
                    DrawPullableProperties();
                    break;
                case DynamicObject.DynamicType.Switchable:
                    DrawSwitchableProperties();
                    break;
                case DynamicObject.DynamicType.Rotable:
                    DrawRotableProperties();
                    break;
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(useEvent1, new GUIContent("Event Settings")))
            {
                EditorGUILayout.PropertyField(useEvent1, new GUIContent("OnOpen"));
                EditorGUILayout.PropertyField(useEvent2, new GUIContent("OnClose"));

                if(interactTypeEnum != DynamicObject.InteractType.Animation)
                    EditorGUILayout.PropertyField(onValueChange, new GUIContent("OnValueChange"));

                if (dynamicStatusEnum == DynamicObject.DynamicStatus.Locked)
                {
                    EditorGUILayout.PropertyField(lockedEvent, new GUIContent("OnLocked"));
                    EditorGUILayout.PropertyField(unlockedEvent, new GUIContent("OnUnlocked"));
                }
                EditorDrawing.EndBorderHeaderLayout();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawDynamicTypeGroup()
        {
            GUIContent[] toolbarContent = {
                new GUIContent(Resources.Load<Texture>("EditorIcons/icon_openable"), "Openable"),
                new GUIContent(Resources.Load<Texture>("EditorIcons/icon_pullable"), "Pullable"),
                new GUIContent(Resources.Load<Texture>("EditorIcons/icon_switchable"), "Switchable"),
                new GUIContent(Resources.Load<Texture>("EditorIcons/icon_rotable"), "Rotable")
            };

            Vector2 prevIconSize = EditorGUIUtility.GetIconSize();
            EditorGUIUtility.SetIconSize(new Vector2(18, 18));

            GUIStyle toolbarButtons = new GUIStyle(GUI.skin.button);
            toolbarButtons.fixedHeight = 0;
            toolbarButtons.fixedWidth = 40;

            Rect toolbarRect = EditorGUILayout.GetControlRect(false, 25);
            toolbarRect.width = 40 * toolbarContent.Length;
            toolbarRect.x = EditorGUIUtility.currentViewWidth / 2 - toolbarRect.width / 2 + 7f;

            dynamicType.enumValueIndex = GUI.Toolbar(toolbarRect, dynamicType.enumValueIndex, toolbarContent, toolbarButtons);

            EditorGUIUtility.SetIconSize(prevIconSize);
        }

        private void DrawOpenableProperties()
        {
            if(interactTypeEnum == DynamicObject.InteractType.Mouse && openableProperties.BoolValue("dragSounds"))
                EditorGUILayout.PropertyField(audioSource);

            switch (interactTypeEnum)
            {
                case DynamicObject.InteractType.Dynamic:
                    EditorGUILayout.PropertyField(m_target);
                    break;
                case DynamicObject.InteractType.Mouse:
                    EditorGUILayout.PropertyField(m_target);
                    EditorGUILayout.PropertyField(joint);
                    EditorGUILayout.PropertyField(rigidbody);
                    break;
                case DynamicObject.InteractType.Animation:
                    EditorGUILayout.PropertyField(m_target);
                    EditorGUILayout.PropertyField(animator); 
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            if (interactTypeEnum != DynamicObject.InteractType.Animation)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(openableProperties["openLimits"], new GUIContent("Dynamic Limits")))
                {
                    openableProperties.Draw("openLimits");

                    float minLimit = openableProperties["openLimits"].FindPropertyRelative("min").floatValue;
                    float maxLimit = openableProperties["openLimits"].FindPropertyRelative("max").floatValue;
                    SerializedProperty startAngle = openableProperties["startingAngle"];
                    startAngle.floatValue = EditorGUILayout.Slider(new GUIContent(startAngle.displayName), startAngle.floatValue, minLimit, maxLimit);
                    EditorGUILayout.Space(1f);

                    openableProperties.Draw("limitsForward");
                    openableProperties.Draw("limitsUpward");
                    EditorDrawing.EndBorderHeaderLayout();
                }

                EditorGUILayout.Space(1f);
            }
            else
            {
                _target.openable.dragSounds = false;
                if (EditorDrawing.BeginFoldoutBorderLayout(openable, new GUIContent("Animation Settings")))
                {
                    EditorGUILayout.PropertyField(useTrigger1, new GUIContent("Open Trigger Name"));
                    EditorGUILayout.PropertyField(useTrigger2, new GUIContent("Close Trigger Name"));
                    if (openableProperties["bothSidesOpen"].boolValue)
                        EditorGUILayout.PropertyField(useTrigger3, new GUIContent("OpenSide Name"));

                    EditorGUILayout.Space();
                    openableProperties.Draw("playCloseSound");
                    if (openableProperties.DrawGetBool("bothSidesOpen"))
                        openableProperties.Draw("openableForward", new GUIContent("Frame Forward"));

                    EditorDrawing.EndBorderHeaderLayout();
                }
            }

            if (interactTypeEnum == DynamicObject.InteractType.Dynamic)
            {
                _target.openable.dragSounds = false;
                if (EditorDrawing.BeginFoldoutBorderLayout(openable, new GUIContent("Dynamic Settings")))
                {
                    openableProperties.Draw("openSpeed");
                    openableProperties.Draw("openCurve");
                    openableProperties.Draw("closeCurve");

                    if (openableProperties.BoolValue("bothSidesOpen"))
                        openableProperties.Draw("openableForward", new GUIContent("Frame Forward"));

                    if (openableProperties.BoolValue("useUpwardDirection"))
                        openableProperties.Draw("openableUp", new GUIContent("Openable Upward"));

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
                    openableProperties.Draw("flipOpenDirection");
                    openableProperties.Draw("flipForwardDirection");
                    openableProperties.Draw("useUpwardDirection");
                    openableProperties.Draw("bothSidesOpen");
                    openableProperties.Draw("showGizmos");
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }
            else if(interactTypeEnum == DynamicObject.InteractType.Mouse)
            {
                _target.openable.bothSidesOpen = true;
                if (EditorDrawing.BeginFoldoutBorderLayout(openable, new GUIContent("Mouse Settings")))
                {
                    openableProperties.Draw("openableForward", new GUIContent("Target Forward"));
                    openableProperties.Draw("openableUp", new GUIContent("Target Upward"));
                    EditorGUILayout.Space();

                    openableProperties.Draw("openSpeed");
                    openableProperties.Draw("damper");
                    if(openableProperties["dragSounds"].boolValue)
                        openableProperties.Draw("dragSoundPlay");
                    EditorGUILayout.Space();

                    openableProperties.Draw("dragSounds");
                    openableProperties.Draw("flipMouse");
                    openableProperties.Draw("flipValue");
                    openableProperties.Draw("showGizmos");
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(openableProperties["useLockedMotion"], new GUIContent("Locked Settings")))
            {
                openableProperties.Draw("useLockedMotion");
                openableProperties.Draw("lockedPattern");
                openableProperties.Draw("lockedMotionAmount");
                openableProperties.Draw("lockedMotionTime");
                EditorDrawing.EndBorderHeaderLayout();
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(ignoreColliders, new GUIContent("Ignore Colliders")))
            {
                ignoreCollidersList.DoLayoutList();
                EditorDrawing.EndBorderHeaderLayout();
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(useSound1, new GUIContent("Dynamic Sounds")))
            {
                if (openableProperties["dragSounds"].boolValue) 
                {
                    openableProperties.Draw("dragSound");
                    EditorGUILayout.Space(2f);
                }

                EditorGUILayout.PropertyField(useSound1, new GUIContent("Open Sound"));
                EditorGUILayout.PropertyField(useSound2, new GUIContent("Close Sound"));

                if (dynamicStatusEnum == DynamicObject.DynamicStatus.Locked)
                {
                    EditorGUILayout.PropertyField(unlockSound);
                    EditorGUILayout.PropertyField(lockedSound);
                }
                EditorDrawing.EndBorderHeaderLayout();
            }
        }

        private void DrawPullableProperties()
        {
            if (interactTypeEnum == DynamicObject.InteractType.Mouse && pullableProperties.BoolValue("dragSounds"))
                EditorGUILayout.PropertyField(audioSource);

            switch (interactTypeEnum)
            {
                case DynamicObject.InteractType.Dynamic:
                case DynamicObject.InteractType.Mouse:
                    EditorGUILayout.PropertyField(m_target);
                    break;
                case DynamicObject.InteractType.Animation:
                    EditorGUILayout.PropertyField(animator);
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            if (interactTypeEnum != DynamicObject.InteractType.Animation)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(pullableProperties["openLimits"], new GUIContent("Dynamic Limits")))
                {
                    pullableProperties.Draw("openLimits");
                    pullableProperties.Draw("pullAxis");
                    EditorDrawing.EndBorderHeaderLayout();
                }

                EditorGUILayout.Space(1f);
            }
            else
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(pullable, new GUIContent("Animation Settings")))
                {
                    EditorGUILayout.PropertyField(useTrigger1, new GUIContent("Open Trigger Name"));
                    EditorGUILayout.PropertyField(useTrigger2, new GUIContent("Close Trigger Name"));
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }

            if (interactTypeEnum == DynamicObject.InteractType.Dynamic)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(pullable, new GUIContent("Dynamic Settings")))
                {
                    pullableProperties.Draw("openCurve");
                    pullableProperties.Draw("openSpeed");
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }
            else if(interactTypeEnum == DynamicObject.InteractType.Mouse)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(pullable, new GUIContent("Mouse Settings")))
                {
                    pullableProperties.Draw("openSpeed");
                    pullableProperties.Draw("damping");
                    if (pullableProperties["dragSounds"].boolValue)
                        pullableProperties.Draw("dragSoundPlay");
                    EditorGUILayout.Space();

                    pullableProperties.Draw("dragSounds");
                    pullableProperties.Draw("flipMouse");
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(ignoreColliders, new GUIContent("Ignore Colliders")))
            {
                EditorGUILayout.PropertyField(ignorePlayerCollider);
                ignoreCollidersList.DoLayoutList();
                EditorDrawing.EndBorderHeaderLayout();
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(useSound1, new GUIContent("Dynamic Sounds")))
            {
                EditorGUILayout.PropertyField(useSound1, new GUIContent("Open Sound"));
                EditorGUILayout.PropertyField(useSound2, new GUIContent("Close Sound"));

                if (dynamicStatusEnum == DynamicObject.DynamicStatus.Locked)
                {
                    EditorGUILayout.PropertyField(unlockSound);
                    EditorGUILayout.PropertyField(lockedSound);
                }
                EditorDrawing.EndBorderHeaderLayout();
            }
        }

        private void DrawSwitchableProperties()
        {
            switch (interactTypeEnum)
            {
                case DynamicObject.InteractType.Dynamic:
                case DynamicObject.InteractType.Mouse:
                    EditorGUILayout.PropertyField(m_target);
                    break;
                case DynamicObject.InteractType.Animation:
                    EditorGUILayout.PropertyField(animator);
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            if (interactTypeEnum != DynamicObject.InteractType.Animation)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(switchableProperties["switchLimits"], new GUIContent("Dynamic Limits")))
                {
                    switchableProperties.Draw("switchLimits");

                    float minLimit = switchableProperties["switchLimits"].FindPropertyRelative("min").floatValue;
                    float maxLimit = switchableProperties["switchLimits"].FindPropertyRelative("max").floatValue;
                    SerializedProperty startAngle = switchableProperties["startingAngle"];
                    startAngle.floatValue = EditorGUILayout.Slider(new GUIContent(startAngle.displayName), startAngle.floatValue, minLimit, maxLimit);

                    switchableProperties.Draw("limitsForward");
                    switchableProperties.Draw("limitsUpward");
                    EditorDrawing.EndBorderHeaderLayout();
                }

                EditorGUILayout.Space(1f);
            }
            else
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(switchable, new GUIContent("Animation Settings")))
                {
                    EditorGUILayout.PropertyField(useTrigger1, new GUIContent("SwitchOn Trigger Name"));
                    EditorGUILayout.PropertyField(useTrigger2, new GUIContent("SwitchOff Trigger Name"));
                    EditorGUILayout.Space();

                    switchableProperties.Draw("lockOnSwitch");

                    EditorDrawing.EndBorderHeaderLayout();
                }
            }

            if (interactTypeEnum == DynamicObject.InteractType.Dynamic)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(switchable, new GUIContent("Dynamic Settings")))
                {
                    switchableProperties.Draw("rootObject");
                    switchableProperties.Draw("switchOnCurve");
                    switchableProperties.Draw("switchOffCurve");
                    switchableProperties.Draw("switchSpeed");
                    EditorGUILayout.Space();

                    switchableProperties.Draw("flipSwitchDirection");
                    switchableProperties.Draw("lockOnSwitch");
                    switchableProperties.Draw("showGizmos");
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }
            else if (interactTypeEnum == DynamicObject.InteractType.Mouse)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(switchable, new GUIContent("Mouse Settings")))
                {
                    switchableProperties.Draw("rootObject");
                    switchableProperties.Draw("switchSpeed");
                    switchableProperties.Draw("damping");
                    EditorGUILayout.Space();

                    switchableProperties.Draw("lockOnSwitch");
                    switchableProperties.Draw("flipMouse");
                    switchableProperties.Draw("showGizmos");
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(ignoreColliders, new GUIContent("Ignore Colliders")))
            {
                ignoreCollidersList.DoLayoutList();
                EditorDrawing.EndBorderHeaderLayout();
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(useSound1, new GUIContent("Dynamic Sounds")))
            {
                EditorGUILayout.PropertyField(useSound1, new GUIContent("SwitchUp Sound"));
                EditorGUILayout.PropertyField(useSound2, new GUIContent("SwitchDown Sound"));

                if (dynamicStatusEnum == DynamicObject.DynamicStatus.Locked)
                {
                    EditorGUILayout.PropertyField(unlockSound);
                    EditorGUILayout.PropertyField(lockedSound);
                }
                EditorDrawing.EndBorderHeaderLayout();
            }
        }

        private void DrawRotableProperties()
        {
            switch (interactTypeEnum)
            {
                case DynamicObject.InteractType.Dynamic:
                case DynamicObject.InteractType.Mouse:
                    EditorGUILayout.PropertyField(m_target);
                    break;
                case DynamicObject.InteractType.Animation:
                    EditorGUILayout.PropertyField(animator);
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            if (interactTypeEnum != DynamicObject.InteractType.Animation)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(rotableProperties["rotationLimit"], new GUIContent("Dynamic Limits")))
                {
                    rotableProperties.Draw("rotationLimit");
                    rotableProperties.Draw("rotateAroundAxis");
                    EditorDrawing.EndBorderHeaderLayout();
                }

                EditorGUILayout.Space(1f);
            }
            else
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(rotable, new GUIContent("Animation Settings")))
                {
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }

            if (interactTypeEnum == DynamicObject.InteractType.Dynamic)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(rotable, new GUIContent("Dynamic Settings")))
                {
                    rotableProperties.Draw("rotateCurve");
                    rotableProperties.Draw("rotationSpeed");
                    EditorGUILayout.Space();

                    rotableProperties.Draw("holdToRotate");
                    rotableProperties.Draw("lockOnRotate");
                    EditorGUILayout.PropertyField(lockPlayer);
                    rotableProperties.Draw("showGizmos");
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }
            else if (interactTypeEnum == DynamicObject.InteractType.Mouse)
            {
                if (EditorDrawing.BeginFoldoutBorderLayout(rotable, new GUIContent("Mouse Settings")))
                {
                    rotableProperties.Draw("rotationSpeed");
                    rotableProperties.Draw("mouseMultiplier");
                    rotableProperties.Draw("damping");
                    EditorGUILayout.Space();

                    rotableProperties.Draw("lockOnRotate");
                    rotableProperties.Draw("showGizmos");
                    EditorDrawing.EndBorderHeaderLayout();
                }
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(ignoreColliders, new GUIContent("Ignore Colliders")))
            {
                ignoreCollidersList.DoLayoutList();
                EditorDrawing.EndBorderHeaderLayout();
            }

            EditorGUILayout.Space(1f);
            if (EditorDrawing.BeginFoldoutBorderLayout(useSound1, new GUIContent("Dynamic Sounds")))
            {
                EditorGUILayout.PropertyField(useSound1, new GUIContent("RotateUp Sound"));
                EditorGUILayout.PropertyField(useSound2, new GUIContent("RotateDown Sound"));

                if (dynamicStatusEnum == DynamicObject.DynamicStatus.Locked)
                {
                    EditorGUILayout.PropertyField(unlockSound);
                    EditorGUILayout.PropertyField(lockedSound);
                }
                EditorDrawing.EndBorderHeaderLayout();
            }
        }
    }
}