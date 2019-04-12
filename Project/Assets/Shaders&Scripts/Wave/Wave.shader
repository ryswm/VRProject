//Texture Distortion tutorial
Shader "Waves" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

		[NoScaleOffset] _FlowMap ("Flow (RG, A noise)", 2D) = "black" {}
		[NoScaleOffset] _DerivHMap ("Deriv (AG) Height (B)", 2D) = "black" {}

		_UJump ("U jump per phase", Range(-0.25, 0.25)) = 0.25
		_VJump ("V jump per phase", Range(-0.25, 0.25)) = 0.25
		_Tiling ("Tiling", Float) = 1
		_Speed ("Speed", Float) = 1
		_FlowStrength ("Flow Strength", Range(0.0, 1.0)) = 1
		_FlowOff ("Flow Offset", Float) = 0

		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_WaveA("Wave A (Direction, Steepness, Wavelength)", Vector) = (1,0,0.5,10)
		_WaveB ("Wave B", Vector) = (0,1,0.15,20)
		_WaveC ("Wave C", Vector) = (0,1,0.15,20)
		_WaveD ("Wave D", Vector) = (0,1,0.15,20)
		_WaveE ("Wave E", Vector) = (0,1,0.15,20)
		_WaveF ("Wave F", Vector) = (0,1,0.15,20)
		_WaveG ("Wave G", Vector) = (0,1,0.10,20)

		_WaveAON("Wave A (Direction, Steepness, Wavelength)", Int) = 0
		_WaveBON ("Wave B", Int) = 0
		_WaveCON ("Wave C", Int) = 0
		_WaveDON ("Wave D", Int) = 0
		_WaveEON ("Wave E", Int) = 0
		_WaveFON ("Wave F", Int) = 0
		_WaveGON ("Wave G", Int) = 0


		_FreqCount("Counter", Float) = 1.0
		_MySpeed("my Speed", Float) = 1.0

		_Tess ("Tessellation", Range(1,32)) = 4
		_Cube ("Cubemap", CUBE) = "" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert addshadow tessellate:tessFixed
		#pragma target 4.5
		
		//Flow Functionality
		#if !defined(FLOW_INCLUDED)
		#define FLOW_INCLUDED
		float3 FlowUVW (float2 uv, float2 flowVector, float2 jump, float flowOff, float tiling, float time, bool flowB){
			float phaseOff = flowB ? 0.5 : 0;
		    float pro = frac(time + phaseOff);
			float3 uvw;
			uvw.xy = uv - flowVector * (pro + flowOff);
			uvw.xy *= tiling;
			uvw.xy += phaseOff;
			uvw.xy += (time = pro) * jump;
			uvw.z = 1 - abs(1 - 2 * pro);
			return uvw;
		}
		#endif

		sampler2D _MainTex, _FlowMap, _DerivHMap;
		float _UJump, _VJump, _Tiling, _Speed, _FlowStrength, _FlowOff;
		float4 _WaveA, _WaveB, _WaveC, _WaveD, _WaveE, _WaveF, _WaveG;
		int _WaveAON, _WaveBON, _WaveCON, _WaveDON, _WaveEON, _WaveFON, _WaveGON;

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _FreqCount;

		float _MySpeed;
		float _Tess;
		samplerCUBE _Cube;
		
		

		float3 UnpackDerivativeHeight(float4 textureData){
			float3 dh = textureData.agb;
			dh.xy = dh.xy * 2 - 1;
			return dh;
		}

		float4 tessFixed()
        {
            return _Tess;
        }

		struct Input {
			float2 uv_MainTex;
			//Emission Map Vars
			/*float3 worldRefl; 
			INTERNAL_DATA*/
		};

		float3 GerstnerWave (
			float4 wave, float3 p, inout float3 tangent, inout float3 binormal
		) {
		    float steepness = wave.z;
		    float wavelength = wave.w;
			float k;
			if(_FreqCount > 0){
				k = 2 * UNITY_PI / (wavelength/_FreqCount);
			} else{
				k = 2 * UNITY_PI / wavelength;
			}

					//Wavelength
			float c = sqrt(9.8 / k);					//Speed
			float2 d = normalize(wave.xy);				//Normals
			float f = k * (dot(d, p.xz) - (c * _MySpeed) * _Time.y); 
			float a = steepness / k;					//Amplitude

			tangent += float3(-d.x * d.x * (steepness * sin(f)), d.x * (steepness * cos(f)), -d.x * d.y * (steepness * sin(f)));
			binormal += float3(-d.x * d.y * (steepness * sin(f)), d.y * (steepness * cos(f)), -d.y * d.y * (steepness * sin(f)));
			return float3(d.x * (a * cos(f)), a * sin(f), d.y * (a * cos(f)));
		}
		

		void vert (inout appdata_full vertexData){
			float3 gridPoint = vertexData.vertex.xyz;
			float3 tangent = float3(1, 0, 0);
			float3 binormal = float3(0, 0, 1);
			float3 p = gridPoint;
			//float3 steep = normalize(float3(_WaveA.z, _WaveB.z, _WaveC.z));

			if(_WaveAON == 1){
			p += GerstnerWave(_WaveA, gridPoint, tangent, binormal);
			}
			if(_WaveBON == 1){
			p += GerstnerWave(_WaveB, gridPoint, tangent, binormal);
			}
			if(_WaveCON == 1){
			p += GerstnerWave(_WaveC, gridPoint, tangent, binormal);
			}
			if(_WaveDON == 1){
			p += GerstnerWave(_WaveD, gridPoint, tangent, binormal);
			}
			if(_WaveEON == 1){
			p += GerstnerWave(_WaveE, gridPoint, tangent, binormal);
			}
			if(_WaveFON == 1){
			p += GerstnerWave(_WaveF, gridPoint, tangent, binormal);
			}
			if(_WaveGON == 1){
			p += GerstnerWave(_WaveG, gridPoint, tangent, binormal);
			}
			float3 normal = normalize(cross(binormal, tangent));
			vertexData.vertex.xyz = p;
			vertexData.normal = normal;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 flowVector = tex2D(_FlowMap, IN.uv_MainTex).rg * 2 - 1;
			flowVector *= _FlowStrength;
			float noise = tex2D(_FlowMap, IN.uv_MainTex).a;
			float time = _Time.y * _Speed + noise;
			float2 jump = float2(_UJump, _VJump);

			float3 uvwA = FlowUVW(IN.uv_MainTex, flowVector, jump, _FlowOff, _Tiling, time, false);
			float3 uvwB = FlowUVW(IN.uv_MainTex, flowVector, jump, _FlowOff, _Tiling, time, true);


			float3 dhA = UnpackDerivativeHeight(tex2D(_DerivHMap, uvwA.xy)) * uvwA.z;
			float3 dhB = UnpackDerivativeHeight(tex2D(_DerivHMap, uvwB.xy)) * uvwB.z;
			o.Normal = normalize(float3(-(dhA.xy + dhB.xy), 1));

			fixed4 texA = tex2D(_MainTex, uvwA.xy) * uvwA.z;
			fixed4 texB = tex2D(_MainTex, uvwB.xy) * uvwB.z;

			fixed4 c = (texA + texB) * _Color;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			//o.Emission = texCUBE (_Cube, WorldReflectionVector (IN, o.Normal)).rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
