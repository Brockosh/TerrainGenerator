Shader "Custom/ColourBlendHeightBased"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // Define minimum and maximum heights for normalization
        _MinHeight ("Minimum Height", Float) = 0.0
        _MaxHeight ("Maximum Height", Float) = 10.0

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

        // Minimum and Maximum Height properties
        float _MinHeight;
        float _MaxHeight;

        // Public color properties
        fixed4 _colorA;
        fixed4 _colorB;

        // Add instancing support for this shader
        UNITY_INSTANCING_BUFFER_START(Props)
            // Additional per-instance properties can be added here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            // Obtain the Y-coordinate (height) of the fragment in world space
            float y = IN.worldPos.y;

            // Normalize the height to a range between 0 and 1
            float t = saturate((y - _MinHeight) / (_MaxHeight - _MinHeight));

            // Perform linear interpolation between colorA and colorB based on the normalized height
            float3 blendedColor = lerp(_colorA, _colorB, t);

            // Assign the blended color to Albedo
            o.Albedo = blendedColor;

            // Assign Metallic and Smoothness from properties
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}