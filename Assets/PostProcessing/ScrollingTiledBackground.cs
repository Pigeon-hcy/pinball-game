using UnityEngine;

[ExecuteAlways]
[AddComponentMenu("Effects/Scrolling Tiled Background")]
public class ScrollingTiledBackground : MonoBehaviour
{
    [Header("Look")]
    public Texture2D iconTexture;
    public Color baseColor = new Color(0.13f, 0.18f, 0.10f, 1f);
    public Color iconColor = new Color(0.08f, 0.11f, 0.06f, 1f);
    [Range(0f, 1f)] public float iconStrength = 0.7f;

    [Header("Tiling & Motion")]
    [Tooltip("图标横/纵向重复次数")]
    public Vector2 tiling = new Vector2(10f, 10f);
    [Tooltip("UV 滚动速度。默认 (-0.02, 0.02) = 图标视觉上从左上向右下移动")]
    public Vector2 scrollSpeed = new Vector2(-0.02f, 0.02f);

    [Header("Placement")]
    public Camera targetCamera;
    [Tooltip("背景距相机的距离。务必大于相机 Near Plane")]
    public float zDepth = 50f;
    public string sortingLayerName = "Default";
    public int sortingOrder = -1000;

    public Shader shader;

    MeshFilter _mf;
    MeshRenderer _mr;
    Material _mat;

    void OnEnable()
    {
        if (shader == null) shader = Shader.Find("Custom/ScrollingTiledBackground");
        EnsureMesh();
        EnsureMaterial();
        Apply();
    }

    void OnDisable()
    {
        if (_mat != null)
        {
            if (Application.isPlaying) Destroy(_mat); else DestroyImmediate(_mat);
            _mat = null;
        }
    }

    void OnValidate()
    {
        if (!isActiveAndEnabled) return;
        EnsureMesh();
        EnsureMaterial();
        Apply();
    }

    void LateUpdate()
    {
        FitToCamera();
    }

    void EnsureMesh()
    {
        _mf = GetComponent<MeshFilter>();
        if (_mf == null) _mf = gameObject.AddComponent<MeshFilter>();
        _mr = GetComponent<MeshRenderer>();
        if (_mr == null) _mr = gameObject.AddComponent<MeshRenderer>();
        if (_mf.sharedMesh == null)
            _mf.sharedMesh = Resources.GetBuiltinResource<Mesh>("Quad.fbx");
        _mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _mr.receiveShadows = false;
        _mr.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        _mr.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
    }

    void EnsureMaterial()
    {
        if (shader == null) shader = Shader.Find("Custom/ScrollingTiledBackground");
        if (shader == null) return;
        if (_mat == null)
        {
            _mat = new Material(shader);
            _mat.hideFlags = HideFlags.DontSave;
        }
        if (_mr != null) _mr.sharedMaterial = _mat;
    }

    void Apply()
    {
        if (_mat != null)
        {
            if (iconTexture != null)
            {
                _mat.SetTexture("_MainTex", iconTexture);
            }
            _mat.SetColor("_BaseColor", baseColor);
            _mat.SetColor("_IconColor", iconColor);
            _mat.SetVector("_Tiling", new Vector4(tiling.x, tiling.y, 0, 0));
            _mat.SetVector("_ScrollSpeed", new Vector4(scrollSpeed.x, scrollSpeed.y, 0, 0));
            _mat.SetFloat("_IconStrength", iconStrength);
        }
        if (_mr != null)
        {
            _mr.sortingLayerName = sortingLayerName;
            _mr.sortingOrder = sortingOrder;
        }
        FitToCamera();
    }

    void FitToCamera()
    {
        var cam = targetCamera != null ? targetCamera : Camera.main;
        if (cam == null) return;

        Vector3 fwd = cam.transform.forward;
        transform.position = cam.transform.position + fwd * zDepth;
        transform.rotation = cam.transform.rotation;

        float h, w;
        if (cam.orthographic)
        {
            h = cam.orthographicSize * 2f;
            w = h * cam.aspect;
        }
        else
        {
            h = 2f * zDepth * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            w = h * cam.aspect;
        }
        transform.localScale = new Vector3(w, h, 1f);
    }
}
