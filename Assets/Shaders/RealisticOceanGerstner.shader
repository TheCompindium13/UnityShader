Shader "Custom/RealisticOceanGerstner"
{
    Properties
    {
        _WaveHeight ("Wave Height", Range(0, 10)) = 1.0
        _WaveSpeed ("Wave Speed", Range(0, 10)) = 1.0
        _WaveFrequency ("Wave Frequency", Range(0, 10)) = 1.0
        _Tiling ("Tiling", Vector) = (1, 1, 0, 0)
        _FoamTex ("Foam Texture", 2D) = "white" {}
        _OceanColor ("Ocean Color", Color) = (0, 0, 1, 1)
        _SkyColor ("Sky Color", Color) = (0.7, 0.7, 0.7, 1)
        _Lambda ("Horizontal Displacement", Range(0, 1)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Uniforms
            float _WaveHeight;
            float _WaveSpeed;
            float _WaveFrequency;
            float4 _Tiling;
            sampler2D _FoamTex;
            float4 _OceanColor;
            float4 _SkyColor;
            float _Lambda;

            // Gerstner Wave Parameters
            float3 _WaveDir1;
            float3 _WaveDir2;
            float3 _WaveDir3;

            // Gerstner Wave Formula
            float gerstnerWave(float3 position, float time, float amplitude, float3 direction, float frequency)
            {
                float k = length(direction); // Wave vector magnitude
                float omega = sqrt(9.8 * k); // Dispersion relation (gravity wave approximation)
                float phase = dot(position, direction) - omega * time;
                return amplitude * cos(phase);
            }

            // Horizontal displacement for Gerstner waves (wave movement in X and Z directions)
            float3 horizontalDisplacement(float3 position, float time, float amplitude, float3 direction, float frequency)
            {
                float k = length(direction); // Wave vector magnitude
                float omega = sqrt(9.8 * k); // Dispersion relation (gravity wave approximation)
                float phase = dot(position, direction) - omega * time;
                float displacement = amplitude * sin(phase); // Sine for horizontal displacement
                return direction * displacement;
            }

            // Vertex shader - Apply Gerstner waves to vertex positions
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

                // Convert UV coordinates to world-space position for wave calculations
                float3 worldPos = float3(v.vertex.x, 0.0, v.vertex.z); // Use X and Z from the vertex, Y is 0 initially.

                // Apply Gerstner wave displacement in the Y axis
                float height = 0.0;
                height += gerstnerWave(worldPos, _Time.y, _WaveHeight, _WaveDir1, _WaveFrequency);
                height += gerstnerWave(worldPos, _Time.y, _WaveHeight, _WaveDir2, _WaveFrequency * 1.5);
                height += gerstnerWave(worldPos, _Time.y, _WaveHeight, _WaveDir3, _WaveFrequency * 2.0);
                v.vertex.y += height;

                // Apply horizontal displacement for wave sharpening (using _Lambda for control)
                float3 displacement = 0.0;
                displacement += horizontalDisplacement(worldPos, _Time.y, _WaveHeight, _WaveDir1, _WaveFrequency);
                displacement += horizontalDisplacement(worldPos, _Time.y, _WaveHeight, _WaveDir2, _WaveFrequency * 1.5);
                displacement += horizontalDisplacement(worldPos, _Time.y, _WaveHeight, _WaveDir3, _WaveFrequency * 2.0);
                v.vertex.xyz += _Lambda * displacement;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader - Apply foam, reflection, and lighting
            fixed4 frag(v2f i) : SV_Target
            {
                // Treat 2D texture coordinates as a 3D position for wave calculation (set z to 0)
                float3 worldPos = float3(i.uv.xy, 0.0);

                // Gerstner wave height calculation for foam and lighting effects
                float height = 0.0;
                height += gerstnerWave(worldPos, _Time.y, _WaveHeight, _WaveDir1, _WaveFrequency);
                height += gerstnerWave(worldPos, _Time.y, _WaveHeight, _WaveDir2, _WaveFrequency * 1.5);
                height += gerstnerWave(worldPos, _Time.y, _WaveHeight, _WaveDir3, _WaveFrequency * 2.0);

                // Foam: Generate foam based on height field using a simple threshold
                fixed4 foamColor = tex2D(_FoamTex, i.uv) * (height > 0.5 ? 1.0 : 0.0);

                // Illumination: Apply reflection and refraction based on wave normals
                float3 normal = normalize(float3(0.0, 1.0, 0.0)); // Simplified normal for this example
                float3 viewDir = normalize(float3(0.0, 1.0, 0.0)); // Viewer direction (assumed)

                // Fresnel term (Schlick's approximation)
                float fresnel = pow(1.0 - dot(viewDir, normal), 5.0);
                fresnel = clamp(fresnel, 0.0, 1.0);

                // Interpolate between ocean color and sky color based on Fresnel reflection
                fixed4 finalColor = lerp(_OceanColor, _SkyColor, fresnel);

                // Combine foam with the final color
                finalColor += foamColor;

                return finalColor;
            }

            ENDHLSL
        }
    }
    Fallback "Diffuse"
}
