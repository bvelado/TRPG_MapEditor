using UnityEditor;
using UnityEngine;

namespace TRPG.Tools {
    public class MapEditor : EditorWindow {

        private MapData currentMapData;
        private SerializedObject currentMap;

        private static MapEditorSettings currentMapEditorSettings;
        private SerializedObject currentSettings;

        private bool toggleTiles = false;
        private Vector2 tilesScrollViewPos = Vector2.zero;

        [MenuItem("Tools/Map Editor %M")]
        static void Init()
        {
            MapEditor window = (MapEditor)EditorWindow.GetWindow(typeof(MapEditor));
            window.titleContent = new GUIContent("Map editor");
            window.Show();

            if(!AssetDatabase.IsValidFolder("Assets/Data"))
            {
                AssetDatabase.CreateFolder("Assets", "Data");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            if(!AssetDatabase.IsValidFolder("Assets/Data/MapEditor"))
            {
                AssetDatabase.CreateFolder("Assets/Data", "MapEditor");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            var settings = AssetDatabase.LoadAssetAtPath<MapEditorSettings>("Assets/Data/MapEditor");

            if(settings != null){
                currentMapEditorSettings = settings;
            } else {
                settings = ScriptableObject.CreateInstance<MapEditorSettings>();
                settings.MapDirectoryPath = "Assets/Resources";

                AssetDatabase.CreateAsset(settings, "Assets/Data/MapEditor/MapEditorSettings.asset");
                currentMapEditorSettings = settings;
            }
        }

        void OnGUI()
        {
            DrawSettingsSection();

            EditorGUILayout.Separator();

            DrawTargetSection();

            if(currentMapData!=null){

                currentMap = new SerializedObject(currentMapData);

                var titleProperty = currentMap.FindProperty("Title");
                var tilesProperty = currentMap.FindProperty("Tiles");

                EditorGUILayout.BeginVertical();

                titleProperty.stringValue = EditorGUILayout.TextField(titleProperty.stringValue);

                DrawTilesSection(tilesProperty);

                EditorGUILayout.EndVertical();

                currentMap.ApplyModifiedProperties();
            }
        }

        #region GUI Drawing

        private void DrawSettingsSection(){
            currentSettings = new SerializedObject(currentMapEditorSettings);

            var mapDirectoryPathProperty = currentSettings.FindProperty("MapDirectoryPath");

            EditorGUILayout.PropertyField(mapDirectoryPathProperty);

            if(!AssetDatabase.IsValidFolder(mapDirectoryPathProperty.stringValue))
                EditorGUILayout.LabelField(mapDirectoryPathProperty.stringValue + " is not a valid path.");

            currentSettings.ApplyModifiedProperties();
        }

        private void DrawTargetSection() {

            EditorGUILayout.BeginHorizontal();
            currentMapData = (MapData)EditorGUILayout.ObjectField(currentMapData, typeof(MapData), false);
            if(GUILayout.Button("New"))
                currentMapData = CreateNewMap();
            EditorGUILayout.EndHorizontal();

            if(currentMapData == null)
                EditorGUILayout.LabelField("No map has been selected. Create a new map or load an existing one to continue.");
        }

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

        #region Map methods

        private MapData CreateNewMap(){
            MapData newMap = ScriptableObject.CreateInstance<MapData>();

            if(AssetDatabase.IsValidFolder(currentMapEditorSettings.MapDirectoryPath))
            {
                AssetDatabase.CreateAsset(newMap, AssetDatabase.GenerateUniqueAssetPath(currentMapEditorSettings.MapDirectoryPath + "/Map.asset"));
                return newMap;
            }
            return newMap;
        }

        #endregion
    }
}


