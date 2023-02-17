
    using System;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(RabbitSpawnController))]
    public class RabbitSpawnControllerInspector : UnityEditor.Editor
    {
        private RabbitSpawnController _spawnController;

        private void Awake()
        {
            _spawnController = target as RabbitSpawnController;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate Footprints"))
            {
                _spawnController.GenerateFootprints();
            }
        }
    }