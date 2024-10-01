Shader "Custom/TextBurnInHDRP"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}      // 字体纹理
        _NoiseTex ("Noise Texture", 2D) = "white" {}    // 噪声纹理，用于控制显现效果
        _Fade ("Fade", Range(0,1)) = 0.0               // 控制渐隐渐显的参数
    }

    SubShader
    {
        Tags { "RenderPipeline" = "HDRenderPipeline" "RenderType" = "Transparent" }
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "Forward" }
            
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
                // 采样字体纹理和噪声纹理
                float4 fontColor = tex2D(_MainTex, i.uv);
                float noise = tex2D(_NoiseTex, i.uv).r;

                // 通过噪声纹理和Fade值控制透明度
                float alpha = smoothstep(_Fade + 0.01, _Fade - 0.01, noise);

                // 将字体的透明度与噪声纹理的控制相结合
                alpha *= fontColor.a;

                // 返回带有透明度的颜色
                return float4(fontColor.rgb, alpha);
            }
            ENDHLSL
        }
    }
    FallBack "Unlit/Transparent"
}
