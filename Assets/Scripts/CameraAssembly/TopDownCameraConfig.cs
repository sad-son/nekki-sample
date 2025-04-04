using System.Collections.Generic;
using UnityEngine;

namespace CameraAssembly
{
    [CreateAssetMenu(menuName = "Data/" + nameof(TopDownCameraConfig), fileName = nameof(TopDownCameraConfig))]
    public class TopDownCameraConfig : ScriptableObject
    {
        [SerializeField] private List<CameraSetting> _cameraSettings;

        public Dictionary<CameraState, CameraSetting> GetSettingsDictionary()
        {
            var result = new Dictionary<CameraState, CameraSetting>();
            foreach (var cameraSetting in _cameraSettings)
            {
                if (!result.TryAdd(cameraSetting.state, cameraSetting))
                {
                    Debug.LogWarning($"Duplicate camera setting: {cameraSetting.state}");
                }
            }
            
            return result;
        }
    }
}