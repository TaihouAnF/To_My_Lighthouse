Shader "Custom/EmissiveRainbow"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}     // 基础纹理（彩虹纹理）
        _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1)  // 自发光颜色
        _Alpha ("Alpha", Range(0, 1)) = 1.0            // Alpha 值
        _FlickerSpeed ("Flicker Speed", Float) = 1.0   // 闪烁速度
        _EmissionStrength ("Emission Strength", Float) = 1.0  // 自发光强度
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _EmissionColor;
            float _Alpha;
            float _FlickerSpeed;
            float _EmissionStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 采样基础颜色纹理
                fixed4 baseColor = tex2D(_MainTex, i.uv);

                // 动态控制Alpha实现闪烁效果
                float flicker = abs(sin(_Time.y * _FlickerSpeed));

                // 设置Alpha值，控制闪烁
                baseColor.a *= _Alpha * flicker;

                // 发光效果，通过纹理和发光颜色相乘，再乘以发光强度
                fixed4 emissiveColor = baseColor * _EmissionColor * _EmissionStrength;

                // 最终颜色 = 基础颜色 + 发光效果
                return fixed4(baseColor.rgb + emissiveColor.rgb, baseColor.a);
            }
            ENDCG
        }
    }

    FallBack "Transparent/Diffuse"
}
