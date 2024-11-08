Shader "Custom/TerrainShader"
{
    Properties
    {
        _SplatMap1 ("SplatMap1", 2D) = "red" {}
        _SplatMap2 ("SplatMap2", 2D) = "red" {}

        _TundraAlbedo ("Albedo Tundra Map", 2D) = "grey" {}
        _TundraHeight ("Height Tundra Map", 2D) = "grey" {}
        _TundraNormal ("Normal Tundra Map", 2D) = "bump" {}
        _TundraNormalScale ("Normal Scale", Float) = 1.0
        _TundraGlossiness ("Smoothness", Range(0,1)) = 0
        _TundraMetallic ("Metallic", Range(0,1)) = 0

        _TaigaAlbedo ("Albedo Taiga Map", 2D) = "grey" {}
        _TaigaHeight ("Height Taiga Map", 2D) = "grey" {}
        _TaigaNormal ("Normal Taiga Map", 2D) = "bump" {}
        _TaigaNormalScale ("Normal Scale", Float) = 1.0
        _TaigaGlossiness ("Smoothness", Range(0,1)) = 0
        _TaigaMetallic ("Metallic", Range(0,1)) = 0.0

        _DesertAlbedo ("Albedo Desert Map", 2D) = "grey" {}
        _DesertHeight ("Height Desert Map", 2D) = "grey" {}
        _DesertNormal ("Normal Desert Map", 2D) = "bump" {}
        _DesertNormalScale ("Normal Scale", Float) = 1.0
        _DesertGlossiness ("Smoothness", Range(0,1)) = 0
        _DesertMetallic ("Metallic", Range(0,1)) = 0.0

        _ForestAlbedo ("Albedo Forest Map", 2D) = "grey" {}
        _ForestHeight ("Height Forest Map", 2D) = "grey" {}
        _ForestNormal ("Normal Forest Map", 2D) = "bump" {}
        _ForestNormalScale ("Normal Scale", Float) = 1.0
        _ForestGlossiness ("Smoothness", Range(0,1)) = 0
        _ForestMetallic ("Metallic", Range(0,1)) = 0.0

        _JungleAlbedo ("Albedo Jungle Map", 2D) = "grey" {}
        _JungleHeight ("Height Jungle Map", 2D) = "grey" {}
        _JungleNormal ("Normal Jungle Map", 2D) = "bump" {}
        _JungleNormalScale ("Normal Scale", Float) = 1.0
        _JungleGlossiness ("Smoothness", Range(0,1)) = 0
        _JungleMetallic ("Metallic", Range(0,1)) = 0.0

        _SavannaAlbedo ("Albedo Savanna Map", 2D) = "grey" {}
        _SavannaHeight ("Height Savanna Map", 2D) = "grey" {}
        _SavannaNormal ("Normal Savanna Map", 2D) = "bump" {}
        _SavannaNormalScale ("Normal Scale", Float) = 1.0
        _SavannaGlossiness ("Smoothness", Range(0,1)) = 0
        _SavannaMetallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:SplatmapsVert addshadow fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #include "GaussianBlur.cginc"
        #include "textureNoTile.cginc"

        sampler2D _SplatMap1;
        float4 _SplatMap1_ST;
        sampler2D _SplatMap2;
        float4 _SplatMap2_ST;

        sampler2D _TundraAlbedo, _TundraHeight, _TundraNormal;
        float4 _TundraAlbedo_ST;
        half _TundraNormalScale, _TundraGlossiness, _TundraMetallic;

        sampler2D _TaigaAlbedo, _TaigaHeight, _TaigaNormal;
        float4 _TaigaAlbedo_ST;
        half _TaigaNormalScale, _TaigaGlossiness, _TaigaMetallic;

        sampler2D _DesertAlbedo, _DesertHeight, _DesertNormal;
        float4 _DesertAlbedo_ST;
        half _DesertNormalScale, _DesertGlossiness, _DesertMetallic;

        sampler2D _ForestAlbedo, _ForestHeight, _ForestNormal;
        float4 _ForestAlbedo_ST;
        half _ForestNormalScale, _ForestGlossiness, _ForestMetallic;

        sampler2D _JungleAlbedo, _JungleHeight, _JungleNormal;
        float4 _JungleAlbedo_ST;
        half _JungleNormalScale, _JungleGlossiness, _JungleMetallic;

        sampler2D _SavannaAlbedo, _SavannaHeight, _SavannaNormal;
        float4 _SavannaAlbedo_ST;
        half _SavannaNormalScale, _SavannaGlossiness, _SavannaMetallic;

        struct Input
        {
            float4 tc;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void SplatmapsVert(inout appdata_full v, out Input data)
        {
            UNITY_INITIALIZE_OUTPUT(Input, data);
            
            data.tc.xy = v.texcoord;
        }
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.tc.xy;
            float2 uv2 = IN.tc.xy;
            fixed4 splat_control = GaussianBlur(_SplatMap1, uv, 15, 2570, 1, 1);
            fixed4 splat_control2 = GaussianBlur(_SplatMap2, uv2, 15, 2570, 1, 1);

            fixed2 uvSplat0 = TRANSFORM_TEX(IN.tc.xy, _TundraAlbedo);
            fixed2 uvSplat1 = TRANSFORM_TEX(IN.tc.xy, _TaigaAlbedo);
            fixed2 uvSplat2 = TRANSFORM_TEX(IN.tc.xy, _DesertAlbedo);
            fixed2 uvSplat3 = TRANSFORM_TEX(IN.tc.xy, _ForestAlbedo);
            fixed2 uvSplat4 = TRANSFORM_TEX(IN.tc.xy, _JungleAlbedo);
            fixed2 uvSplat5 = TRANSFORM_TEX(IN.tc.xy, _SavannaAlbedo);

            NoTileUVs ntuvs0 = textureNoTileCalcUVs(uvSplat0);
            NoTileUVs ntuvs1 = textureNoTileCalcUVs(uvSplat1);
            NoTileUVs ntuvs2 = textureNoTileCalcUVs(uvSplat2);
            NoTileUVs ntuvs3 = textureNoTileCalcUVs(uvSplat3);
            NoTileUVs ntuvs4 = textureNoTileCalcUVs(uvSplat4);
            NoTileUVs ntuvs5 = textureNoTileCalcUVs(uvSplat5);

            fixed texture0Height = textureNoTile(_TundraHeight, ntuvs0);
            fixed texture1Height = textureNoTile(_TaigaHeight, ntuvs1);
            fixed texture2Height = textureNoTile(_DesertHeight, ntuvs2);
            fixed texture3Height = textureNoTile(_ForestHeight, ntuvs3);
            fixed texture4Height = textureNoTile(_JungleHeight, ntuvs4);
            fixed texture5Height = textureNoTile(_SavannaHeight, ntuvs5);

            fixed4 albedo0 = textureNoTile(_TundraAlbedo, ntuvs0);
            fixed4 albedo1 = textureNoTile(_TaigaAlbedo, ntuvs1);
            fixed4 albedo2 = textureNoTile(_DesertAlbedo, ntuvs2);
            fixed4 albedo3 = textureNoTile(_ForestAlbedo, ntuvs3);
            fixed4 albedo4 = textureNoTile(_JungleAlbedo, ntuvs4);
            fixed4 albedo5 = textureNoTile(_SavannaAlbedo, ntuvs5);

            fixed3 normal0 = textureNoTileNormal(_TundraNormal, ntuvs0, _TundraNormalScale);
            fixed3 normal1 = textureNoTileNormal(_TaigaNormal, ntuvs1, _TaigaNormalScale);
            fixed3 normal2 = textureNoTileNormal(_DesertNormal, ntuvs2, _DesertNormalScale);
            fixed3 normal3 = textureNoTileNormal(_ForestNormal, ntuvs3, _ForestNormalScale);
            fixed3 normal4 = textureNoTileNormal(_JungleNormal, ntuvs4, _JungleNormalScale);
            fixed3 normal5 = textureNoTileNormal(_SavannaNormal, ntuvs5, _SavannaNormalScale);
            
            float4 mixedAlbedo = albedo0 * splat_control.r 
                               + albedo1 * splat_control.g 
                               + albedo2 * splat_control.b 
                               + albedo3 * splat_control2.r 
                               + albedo4 * splat_control2.g 
                               + albedo5 * splat_control2.b;
            float mixedNormal = normal0 * splat_control.r 
                               + normal1 * splat_control.g
                               + normal2 * splat_control.b 
                               + normal3 * splat_control2.r 
                               + normal4 * splat_control2.g 
                               + normal5 * splat_control2.b;
            mixedNormal += 1e-5; // avoid zero

            o.Albedo = mixedAlbedo;
            o.Normal = mixedNormal + 0.5;
            o.Smoothness = _TundraGlossiness;
            o.Metallic = _TundraMetallic;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
