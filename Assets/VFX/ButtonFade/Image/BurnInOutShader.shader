Shader "Custom/BurnInOutShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}          // 背景纹理
        _NoiseTex ("Noise Texture", 2D) = "white" {}   // 噪声纹理，用于模拟灰烬效果
        _Fade ("Fade", Range(0,1)) = 0.0              // 控制透明度渐变
    }

    SubShader
    {
        Tags { "RenderPipeline" = "HDRenderPipeline" "RenderType" = "Transparent" }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "Forward" }

            // 透明度混合设置
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _Fade;

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
                // 采样纹理和噪声纹理
                float4 color = tex2D(_MainTex, i.uv);
                float noise = tex2D(_NoiseTex, i.uv).r;

                // 根据噪声和Fade值控制透明度
                float alpha = smoothstep(_Fade + 0.1, _Fade - 0.1, noise);
                alpha = lerp(0.0, 0.02, alpha);  // 控制透明度从0到0.5

                return float4(color.rgb, alpha);  // 将alpha应用到颜色
            }
            ENDHLSL
        }
    }
    FallBack "Unlit/Transparent"
}
