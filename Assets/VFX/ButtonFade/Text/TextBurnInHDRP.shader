Shader "Custom/TextBurnInHDRP"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}         // 字体纹理
        _NoiseTex ("Noise Texture", 2D) = "white" {}       // 噪声纹理
        _Fade ("Fade", Range(0,1)) = 0.0                  // 渐入控制
    }

    SubShader
    {
        Tags { "RenderPipeline" = "HDRenderPipeline" "RenderType" = "Transparent" }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "Forward" }

            Blend SrcAlpha OneMinusSrcAlpha  // 标准透明度混合模式
            Cull Off
            ZWrite Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _Fade;
            float4 _Color;  // 从 TextMeshPro 材质传入的颜色

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // 采样字体纹理和噪声纹理
                float4 fontTexColor = tex2D(_MainTex, i.uv);   // 字体的纹理颜色
                float noise = tex2D(_NoiseTex, i.uv).r;        // 噪声值

                // 使用 Fade 值和噪声控制透明度
                float alpha = smoothstep(_Fade + 0.1, _Fade - 0.1, noise);

                // 将透明度应用到字体纹理
                float finalAlpha = fontTexColor.a * alpha;

                // 最终颜色是传入的颜色乘以字体纹理的颜色
                return float4(_Color.rgb * fontTexColor.rgb, finalAlpha);
            }
            ENDHLSL
        }
    }
    FallBack "Unlit/Transparent"
}
