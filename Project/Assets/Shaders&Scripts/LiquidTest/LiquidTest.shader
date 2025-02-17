﻿//Texture Distortion tutorial
Shader "Custom/LiquidTest" {
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
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma target gl4.1
		
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

		float3 UnpackDerivativeHeight(float4 textureData){
			float3 dh = textureData.agb;
			dh.xy = dh.xy * 2 - 1;
			return dh;
		}

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 flowVector = tex2D(_FlowMap, IN.uv_MainTex).rg * 2 - 1;
			flowVector *= _FlowStrength;
			float noise = tex2D(_FlowMap, IN.uv_MainTex).a;
			float time = _Time.y * _Speed + noise;
			float2 jump = float2(_UJump, _VJump);

			float3 uvwA = FlowUVW(IN.uv_MainTex, flowVector, jump, _FlowOff, _Tiling, time, false);
			float3 uvwB = FlowUVW(IN.uv_MainTex, flowVector, jump, _FlowOff, _Tiling, time, true);

			//float3 normalA = UnpackNormal(tex2D(_NormalMap, uvwA.xy)) * uvwA.z;
			//float3 normalB = UnpackNormal(tex2D(_NormalMap, uvwB.xy)) * uvwB.z;
			//o.Normal = normalize(normalA + normalB);

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
		}
		ENDCG
	}
	FallBack "Diffuse"
}
