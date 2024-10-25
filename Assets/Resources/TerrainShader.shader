Shader "Custom/TerrainShader"
{
    Properties
    {
        _SplatMap1 ("SplatMap1", 2D) = "white" {}
        _SplatMap2 ("SplatMap2", 2D) = "white" {}

        _TundraAlbedo ("Albedo Tundra Map", 2D) = "grey" {}
        _TundraHeight ("Height Tundra Map", 2D) = "grey" {}
        _TundraNormal ("Normal Tundra Map", 2D) = "bump" {}
        _TundraNormalScale ("Normal Scale", Float) = 1.0
        _TundraGlossiness ("Smoothness", Range(0,1)) = 0.2
        _TundraMetallic ("Metallic", Range(0,1)) = 0.2

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

        sampler2D _TundraAlbedo, _TundraHeight, _TundraNormal;
        half _TundraNormalScale, _TundraGlossiness, _TundraMetallic;

        sampler2D _TaigaAlbedo, _TaigaHeight, _TaigaNormal;
        half _TaigaNormalScale, _TaigaGlossiness, _TaigaMetallic;

        sampler2D _DesertAlbedo, _DesertHeight, _DesertNormal;
        half _DesertNormalScale, _DesertGlossiness, _DesertMetallic;

        sampler2D _ForestAlbedo, _ForestHeight, _ForestNormal;
        half _ForestNormalScale, _ForestGlossiness, _ForestMetallic;

        sampler2D _JungleAlbedo, _JungleHeight, _JungleNormal;
        half _JungleNormalScale, _JungleGlossiness, _JungleMetallic;

        sampler2D _SavannaAlbedo, _SavannaHeight, _SavannaNormal;
        half _SavannaNormalScale, _SavannaGlossiness, _SavannaMetallic;

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
            fixed4 c = tex2D(_TundraAlbedo, IN.uv_TundraAlbedo);
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
