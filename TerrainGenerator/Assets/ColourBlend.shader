Shader "Custom/ColourBlendHeightBased"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // Define minimum and maximum heights for normalization
        minHeight ("Minimum Height", Float) = 0.0
        maxHeight ("Maximum Height", Float) = 10.0

        _colorA ("Top Colour", Color) = (0, 1, 0, 1)
        _colorB ("Bottom Colour", Color) = (1, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Use Unity's Standard lighting model with full forward shadows
        #pragma surface surf Standard fullforwardshadows

        // Target shader model 3.0 for better lighting
        #pragma target 3.0

        sampler2D _MainTex;

        // Structure to receive data from the vertex shader
        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos; // World position of the fragment
        };


        // Shader properties
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        const static int maxColourCount = 8;
        const static float epsilon = 1E-4;

        int colourCount;
        float3 colours[maxColourCount];
        float colourStartHeights[maxColourCount];
        float baseBlends[maxColourCount];


        // Minimum and Maximum Height properties
        float minHeight;
        float maxHeight;



        float inverseLerp(float a, float b, float value)
        {
            return saturate((value - a) / (b - a));
        }


        // Add instancing support for this shader
        UNITY_INSTANCING_BUFFER_START(Props)
            // Additional per-instance properties can be added here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float heightPercent = inverseLerp(minHeight, maxHeight, IN.worldPos.y);

            for (int i = 0; i < colourCount; i++)
            {
                float drawStrength = inverseLerp(-baseBlends[i] / 2 - epsilon, baseBlends[i] / 2, heightPercent - colourStartHeights[i]);
                o.Albedo = o.Albedo * (1 - drawStrength) + colours[i] * drawStrength;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}