Shader "Custom/ScrollingTiledBackground"
{
    Properties
    {
        _MainTex ("Icon (alpha used)", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (0.13, 0.18, 0.10, 1)
        _IconColor ("Icon Color", Color) = (0.08, 0.11, 0.06, 1)
        _Tiling ("Tiling (x,y)", Vector) = (10, 10, 0, 0)
        _ScrollSpeed ("UV Scroll Speed (x,y)", Vector) = (-0.02, 0.02, 0, 0)
        _IconStrength ("Icon Strength", Range(0, 1)) = 0.7
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Opaque" "IgnoreProjector"="True" }
        Cull Off ZWrite On Lighting Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float4 pos : SV_POSITION; float2 uv : TEXCOORD0; };

            sampler2D _MainTex;
            float4 _BaseColor;
            float4 _IconColor;
            float4 _Tiling;
            float4 _ScrollSpeed;
            float _IconStrength;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv * _Tiling.xy + _ScrollSpeed.xy * _Time.y;
                fixed4 icon = tex2D(_MainTex, uv);
                // Mask = icon's alpha (transparent png) OR brightness fallback
                float mask = icon.a;
                mask *= _IconStrength;
                fixed3 col = lerp(_BaseColor.rgb, _IconColor.rgb, mask);
                return fixed4(col, 1);
            }
            ENDCG
        }
    }
}
