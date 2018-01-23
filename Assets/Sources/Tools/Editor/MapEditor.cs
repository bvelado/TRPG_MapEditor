using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TRPG.Tools {
    [CustomEditor(typeof(MapData))]
    public class MapEditor : Editor {

        public GameObject tilePrefab;

        private MapEditorSettings currentSettings;
        private SerializedObject currentMap;

        private bool toggleTiles = false;
        private Vector2 tilesScrollViewPos = Vector2.zero;

        private void OnEnable()
        {
            SceneView.onSceneGUIDelegate = DrawSceneView;
            if (SceneView.lastActiveSceneView) SceneView.lastActiveSceneView.Repaint();
        }

        private void OnDisable()
        {
            SceneView.onSceneGUIDelegate = null;
        }

        public override void OnInspectorGUI() {

            LoadMapData();
            LoadSettingsDataOrCreateNew();

            var mapTitleProp = currentMap.FindProperty("Title");
            mapTitleProp.stringValue = EditorGUILayout.TextField("Title", mapTitleProp.stringValue);

            currentMap.ApplyModifiedProperties();
        }

        private void DrawSceneView(SceneView sceneView) {

            LoadMapData();
            LoadSettingsDataOrCreateNew();

            DrawSceneGrid(sceneView);
            DrawAddTileHandles(sceneView);

            Handles.BeginGUI();

            DrawSceneViewOverlay(sceneView);
            DrawSceneToolbarGUI(sceneView);

            Handles.EndGUI();
        }

        #region GUI Drawing

        private void DrawTilesSection(SerializedProperty tilesProperty) {

            EditorGUILayout.BeginHorizontal();
            toggleTiles = EditorGUILayout.Foldout(toggleTiles, "Tiles", true);
            tilesProperty.arraySize = EditorGUILayout.IntField("Length", tilesProperty.arraySize);
            EditorGUILayout.EndHorizontal();
            
            if(toggleTiles){
                tilesScrollViewPos = EditorGUILayout.BeginScrollView(tilesScrollViewPos);
                for(int i = 0; i < tilesProperty.arraySize; i++){
                    EditorGUILayout.PropertyField(tilesProperty.GetArrayElementAtIndex(i));
                }
                EditorGUILayout.EndScrollView();
            }
        }

        #endregion

        #region SceneGUI Drawing

        private void DrawSceneToolbarGUI(SceneView sceneView) {
            Rect toolbarRect = new Rect(0f, sceneView.position.height - EditorGUIUtility.singleLineHeight - 18f, sceneView.position.width, EditorGUIUtility.singleLineHeight);

            GUI.BeginGroup(toolbarRect, EditorStyles.toolbar);
            GUI.Label(new Rect(0f,0f, 160f, EditorGUIUtility.singleLineHeight), currentMap.FindProperty("Title").stringValue, EditorStyles.miniLabel);
            GUI.EndGroup();
        }

        private void DrawSceneViewOverlay(SceneView sceneView) {
            Rect overlayRect = sceneView.position;

            var mapTitleStyle = new GUIStyle(EditorStyles.boldLabel);
            mapTitleStyle.fontSize = 21;

            GUILayout.BeginArea(new Rect(10f, 10f, 300f, 54f));
            GUILayout.Label(currentMap.FindProperty("Title").stringValue, mapTitleStyle);
            GUILayout.EndArea();

        }

        #endregion

        #region Scene Drawing

        private void DrawSceneGrid(SceneView sceneView){
            
            List<Vector3> lines = new List<Vector3>();

            for(int x = -currentSettings.MaxWidth / 2; x < currentSettings.MaxWidth / 2 + 1; x++){
                lines.Add(new Vector3(x, 0f, -currentSettings.MaxDepth/2));
                lines.Add(new Vector3(x, 0f, currentSettings.MaxDepth/2));
            }

            for(int z = -currentSettings.MaxDepth / 2; z < currentSettings.MaxDepth / 2 + 1; z++){
                lines.Add(new Vector3(currentSettings.MaxWidth / 2, 0f, z));
                lines.Add(new Vector3(-currentSettings.MaxWidth / 2, 0f, z));
            }

            Handles.color = Color.white;
            Handles.DrawLines(lines.ToArray());
        }

        private void DrawAddTileHandles(SceneView sceneView) {

            if(currentMap.FindProperty("Tiles").arraySize > 0){
                // Iterate over the tiles array
            } else {
                Handles.color = Color.red;
                Handles.SphereHandleCap(0, Vector3.up, Quaternion.identity, 1f, EventType.Repaint);
            }

        }

        #endregion

        #region Map methods

        private MapData CreateNewMap(){
            MapData newMap = ScriptableObject.CreateInstance<MapData>();
            var folderPath = currentSettings.MapDirectoryPath;

            if(AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateAsset(newMap, AssetDatabase.GenerateUniqueAssetPath(folderPath + "/Map.asset"));
                return newMap;
            }
            return newMap;
        }

        #endregion

        #region Settings data

        private void LoadSettingsDataOrCreateNew() {
            if(currentSettings != null) return;

            currentSettings = AssetDatabase.LoadAssetAtPath<MapEditorSettings>("Assets/Data/MapEditor/MapEditorSettings.asset");

            if(currentSettings == null)
                Debug.LogError("The map editor settings doesn't exists. It should be located in Assets/Data/MapEditor/MapEditorSettings.asset.");
        }

        #endregion

        private void LoadMapData(){
            if(target != null) {
                if(currentMap == null)
                    currentMap = new SerializedObject((MapData)target);
            }
        }
    }
}


