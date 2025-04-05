using AbilitiesAssembly;
using ServiceLocatorSystem;
using UnityEngine;

public class GizmosDebugger : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (!ServiceLocatorController.TryResolve<AbilitiesSystemContainer>(out var abilitiesSystemContainer)) return;
        
        var selector = abilitiesSystemContainer.Resolve<TargetSelector>();
        var ray = selector.Ray;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * Camera.main.farClipPlane); 
    }
}