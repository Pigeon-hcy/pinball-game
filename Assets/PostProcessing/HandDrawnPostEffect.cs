using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Hand Drawn Post Effect")]
public class HandDrawnPostEffect : MonoBehaviour
{
    public Shader shader;

    [Header("Edge Wobble")]
    [Range(0f, 0.02f)] public float jitterStrength = 0.003f;
    [Range(1f, 200f)] public float noiseScale = 60f;

    [Header("Boil")]
    [Tooltip("帧率化的抖动速度，模拟逐帧手绘。0 = 静态毛边")]
    [Range(0f, 30f)] public float boilFps = 8f;

    [Header("Inked Outline")]
    [Range(0f, 2f)] public float edgeDarken = 0.6f;
    [Range(0f, 0.5f)] public float edgeThreshold = 0.05f;

    [Header("Paper / Color")]
    [Range(0f, 0.2f)] public float grainStrength = 0.04f;
    [Range(0.5f, 2f)] public float saturation = 1.05f;

    Material _mat;

    void OnEnable()
    {
        if (shader == null)
            shader = Shader.Find("Hidden/HandDrawnPost");
    }

    void OnDisable()
    {
        if (_mat != null)
        {
            if (Application.isPlaying) Destroy(_mat);
            else DestroyImmediate(_mat);
            _mat = null;
        }
    }

    Material GetMaterial()
    {
        if (shader == null || !shader.isSupported) return null;
        if (_mat == null)
        {
            _mat = new Material(shader);
            _mat.hideFlags = HideFlags.HideAndDontSave;
        }
        return _mat;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        var m = GetMaterial();
        if (m == null)
        {
            Graphics.Blit(src, dst);
            return;
        }

        m.SetFloat("_JitterStrength", jitterStrength);
        m.SetFloat("_NoiseScale", noiseScale);
        m.SetFloat("_GrainStrength", grainStrength);
        m.SetFloat("_TimeStep", boilFps);
        m.SetFloat("_EdgeStrength", edgeDarken);
        m.SetFloat("_EdgeThreshold", edgeThreshold);
        m.SetFloat("_Saturation", saturation);

        Graphics.Blit(src, dst, m);
    }
}
