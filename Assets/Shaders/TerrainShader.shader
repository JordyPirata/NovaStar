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
        _TundraGlossiness ("Smoothness", Range(0,1)) = 0.1
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
        #pragma surface surf Standard vertex:SplatmapsVert addshadow fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #include "textureNoTile.cginc"

        UNITY_DECLARE_TEX2D(_SplatMap1);
        UNITY_DECLARE_TEX2D(_SplatMap2);

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
            fixed4 splat_control = UNITY_SAMPLE_TEX2D(_SplatMap1, IN.tc.xy);

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

            
            
            // Albedo comes from a texture tinted by color
            //fixed4 c = tex2D(_TundraAlbedo, ntuvs0);
            //fixed4 c = tex2D(_TundraAlbedo, IN.uv_TundraAlbedo);
            o.Albedo = albedo0.rgb;
            o.Alpha = albedo0.a;
            o.Normal = normal0;
            // Metallic and smoothness come from slider variables
            o.Metallic = _TundraMetallic;
            o.Smoothness = _TundraGlossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
