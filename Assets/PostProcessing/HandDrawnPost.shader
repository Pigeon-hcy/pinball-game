Shader "Hidden/HandDrawnPost"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _JitterStrength ("Jitter Strength", Float) = 0.003
        _NoiseScale ("Noise Scale", Float) = 60
        _GrainStrength ("Grain Strength", Float) = 0.04
        _TimeStep ("Boil FPS", Float) = 8
        _EdgeStrength ("Edge Darken", Float) = 0.6
        _EdgeThreshold ("Edge Threshold", Float) = 0.05
        _Saturation ("Saturation", Float) = 1.05
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float _JitterStrength;
            float _NoiseScale;
            float _GrainStrength;
            float _TimeStep;
            float _EdgeStrength;
            float _EdgeThreshold;
            float _Saturation;

            float hash21(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(p.x * p.y);
            }

            float vnoise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                float a = hash21(i);
                float b = hash21(i + float2(1, 0));
                float c = hash21(i + float2(0, 1));
                float d = hash21(i + float2(1, 1));
                float2 u = f * f * (3.0 - 2.0 * f);
                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }

            float luma(float3 c)
            {
                return dot(c, float3(0.299, 0.587, 0.114));
            }

            fixed4 frag(v2f_img i) : SV_Target
            {
                // Quantize time so the noise "boils" at a fixed FPS,
                // mimicking the pop of frame-by-frame hand animation.
                // _TimeStep == 0 freezes it (static fuzz).
                float t = _TimeStep > 0.001 ? floor(_Time.y * _TimeStep) : 0.0;

                float2 nuv = i.uv * _NoiseScale + t * 17.137;
                float nx = vnoise(nuv) - 0.5;
                float ny = vnoise(nuv + 7.31) - 0.5;
                float2 jitter = float2(nx, ny) * _JitterStrength;

                float2 uv = i.uv + jitter;
                fixed4 col = tex2D(_MainTex, uv);

                // Sobel edge on luma — darken edges to fake an inked outline.
                float2 ts = _MainTex_TexelSize.xy;
                float l00 = luma(tex2D(_MainTex, uv + ts * float2(-1,-1)).rgb);
                float l10 = luma(tex2D(_MainTex, uv + ts * float2( 0,-1)).rgb);
                float l20 = luma(tex2D(_MainTex, uv + ts * float2( 1,-1)).rgb);
                float l01 = luma(tex2D(_MainTex, uv + ts * float2(-1, 0)).rgb);
                float l21 = luma(tex2D(_MainTex, uv + ts * float2( 1, 0)).rgb);
                float l02 = luma(tex2D(_MainTex, uv + ts * float2(-1, 1)).rgb);
                float l12 = luma(tex2D(_MainTex, uv + ts * float2( 0, 1)).rgb);
                float l22 = luma(tex2D(_MainTex, uv + ts * float2( 1, 1)).rgb);

                float gx = -l00 - 2.0*l01 - l02 + l20 + 2.0*l21 + l22;
                float gy = -l00 - 2.0*l10 - l20 + l02 + 2.0*l12 + l22;
                float edge = saturate(sqrt(gx*gx + gy*gy) - _EdgeThreshold);
                col.rgb *= 1.0 - edge * _EdgeStrength;

                // High-frequency paper grain.
                float grain = vnoise(i.uv * 800.0 + t * 3.7) - 0.5;
                col.rgb += grain * _GrainStrength;

                // Slight saturation push — hand-painted art tends to read
                // a touch more saturated than pixel-perfect renders.
                float l = luma(col.rgb);
                col.rgb = lerp(float3(l,l,l), col.rgb, _Saturation);

                return col;
            }
            ENDCG
        }
    }
    FallBack Off
}
