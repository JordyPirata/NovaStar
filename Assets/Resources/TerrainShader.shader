Shader "Custom/TerrainShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)

        _TundraAlbedo ("Albedo Tundra Map", 2D) = "grey" {}
        _TundraHeight ("Height Tundra Map", 2D) = "grey" {}
        _TundraNormal ("Normal Tundra Map", 2D) = "bump" {}
        _TundraNormalScale ("Normal Scale", Float) = 1.0
        _TundraGlossiness ("Smoothness", Range(0,1)) = 0.5
        _TundraMetallic ("Metallic", Range(0,1)) = 0.0

        _TaigaAlbedo ("Albedo Taiga Map", 2D) = "grey" {}
        _TaigaHeight ("Height Taiga Map", 2D) = "grey" {}
        _TaigaNormal ("Normal Taiga Map", 2D) = "bump" {}
        _TaigaNormalScale ("Normal Scale", Float) = 1.0
        _TaigaGlossiness ("Smoothness", Range(0,1)) = 0.5
        _TaigaMetallic ("Metallic", Range(0,1)) = 0.0

        _DesertAlbedo ("Albedo Desert Map", 2D) = "grey" {}
        _DesertHeight ("Height Desert Map", 2D) = "grey" {}
        _DesertNormal ("Normal Desert Map", 2D) = "bump" {}
        _DesertNormalScale ("Normal Scale", Float) = 1.0
        _DesertGlossiness ("Smoothness", Range(0,1)) = 0.5
        _DesertMetallic ("Metallic", Range(0,1)) = 0.0

        _ForestAlbedo ("Albedo Forest Map", 2D) = "grey" {}
        _ForestHeight ("Height Forest Map", 2D) = "grey" {}
        _ForestNormal ("Normal Forest Map", 2D) = "bump" {}
        _ForestNormalScale ("Normal Scale", Float) = 1.0
        _ForestGlossiness ("Smoothness", Range(0,1)) = 0.5
        _ForestMetallic ("Metallic", Range(0,1)) = 0.0

        _JungleAlbedo ("Albedo Jungle Map", 2D) = "grey" {}
        _JungleHeight ("Height Jungle Map", 2D) = "grey" {}
        _JungleNormal ("Normal Jungle Map", 2D) = "bump" {}
        _JungleNormalScale ("Normal Scale", Float) = 1.0
        _JungleGlossiness ("Smoothness", Range(0,1)) = 0.5
        _JungleMetallic ("Metallic", Range(0,1)) = 0.0
        
        _SavannaAlbedo ("Albedo Savanna Map", 2D) = "grey" {}
        _SavannaHeight ("Height Savanna Map", 2D) = "grey" {}
        _SavannaNormal ("Normal Savanna Map", 2D) = "bump" {}
        _SavannaNormalScale ("Normal Scale", Float) = 1.0
        _SavannaGlossiness ("Smoothness", Range(0,1)) = 0.5
        _SavannaMetallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        fixed4 _Color;

        sampler2D _TundraAlbedo;
        sampler2D _TundraHeight;
        sampler2D _TundraNormal;
        float _TundraNormalScale;
        float _TundraGlossiness;
        float _TundraMetallic;

        sampler2D _TaigaAlbedo;
        sampler2D _TaigaHeight;
        sampler2D _TaigaNormal;
        float _TaigaNormalScale;
        float _TaigaGlossiness;
        float _TaigaMetallic;

        sampler2D _DesertAlbedo;
        sampler2D _DesertHeight;
        sampler2D _DesertNormal;
        float _DesertNormalScale;
        float _DesertGlossiness;
        float _DesertMetallic;

        sampler2D _ForestAlbedo;
        sampler2D _ForestHeight;
        sampler2D _ForestNormal;
        float _ForestNormalScale;
        float _ForestGlossiness;
        float _ForestMetallic;

        sampler2D _JungleAlbedo;
        sampler2D _JungleHeight;
        sampler2D _JungleNormal;
        float _JungleNormalScale;
        float _JungleGlossiness;
        float _JungleMetallic;

        sampler2D _SavannaAlbedo;
        sampler2D _SavannaHeight;
        sampler2D _SavannaNormal;
        float _SavannaNormalScale;
        float _SavannaGlossiness;
        float _SavannaMetallic;

        struct Input
        {
            float2 uv_TundraAlbedo;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_TundraAlbedo, IN.uv_TundraAlbedo) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Normal = UnpackNormal(tex2D(_TundraNormal, IN.uv_TundraAlbedo));
            // Metallic and smoothness come from slider variables
            o.Metallic = _TundraMetallic;
            o.Smoothness = _TundraGlossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
