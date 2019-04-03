Shader "Custom/LiquidTest" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[NoScaleOffset] _FlowMap ("Flow (RG, A noise)", 2D) = "black" {}
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
		float3 FlowUVW (float2 uv, float2 flowVector, float time){
		    float pro = frac(time);
			float3 uvw;
			uvw.xy = uv - flowVector * pro;
			uvw.z = 1 - abs(1 - 2 * pro);
			return uvw;
		}
		#endif

		sampler2D _MainTex, _FlowMap;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 flowVector = tex2D(_FlowMap, IN.uv_MainTex).rg * 2 - 1;
			float noise = tex2D(_FlowMap, IN.uv_MainTex).a;
			float time = _Time.y + noise;
			float3 uvw = FlowUVW(IN.uv_MainTex, flowVector, time);
			fixed4 c = tex2D (_MainTex, uvw.xy) * uvw.z * _Color;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
