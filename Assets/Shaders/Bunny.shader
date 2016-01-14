Shader "Custom/Bunny" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_HeightMap("HeightMap", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amount("Amount", Range(0, 1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _HeightMap;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		float _Amount;
		fixed4 _Color;

		void vert(inout appdata_full v)
		{
			float2 uv = v.texcoord;
			float t = _Time * 1.0;
			uv.x += t;
			uv.y += t;
			float lum = tex2Dlod(_HeightMap, float4(uv, 0.0, 0.0)).r;
			v.vertex.xyz += v.normal * lum * _Amount;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float2 uv = IN.uv_MainTex;
			uv.x += _Time * 1.0;
			uv.y += _Time * 1.5;
			fixed4 c = tex2D(_MainTex, uv) * _Color;

			uv = IN.uv_MainTex;
			uv.x -= _Time * 0.3;
			uv.y -= _Time * 0.4;
			fixed4 c2 = tex2D(_MainTex, uv) * _Color;

			o.Albedo = lerp(c.rgb, c2.rgb, 0.5);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
