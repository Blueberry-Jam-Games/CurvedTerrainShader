using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class CustomFrustumCulling : MonoBehaviour
{

    private const string BENDING_FEATURE = "_ENABLE_BENDING";

    private void Awake()
    {
        Debug.Log("Awake");
        if (Application.isPlaying)
        {
            Debug.Log("Playing");
            Shader.EnableKeyword(BENDING_FEATURE);
        }
        else
        {
            Debug.Log("Disabling");
            Shader.DisableKeyword(BENDING_FEATURE);
            //Shader.EnableKeyword(BENDING_FEATURE);
        }
    }

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
        cam.cullingMatrix = Matrix4x4.Ortho(-99, 99, -99, 99, 0.001f, 99) * cam.worldToCameraMatrix;
    }

    private void OnEndCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
        cam.ResetCullingMatrix();
    }
}
