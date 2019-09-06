Shader "Custom/Inverted_Hull" {
	Properties
	{
		_OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineThickness ("Outline Thickness", Range(0,0.1)) = 0.03

		_Color ("Color", Color) = (0, 0, 0, 1)
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }

		// first pass to render the object itself
		Pass 
		{
			CGPROGRAM
			#include "UNITYCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _Color;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col *= _Color;
				return col;
			}
			ENDCG
		}

		// second pass to render outlines
		Pass
		{

			CGPROGRAM
			#include "UNITYCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _Color;

			fixed4 _OutlineColor;
			fixed4 _OutlineThickness;

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				float3 normal = normalize(v.normal);
				float3 outlineOffset = normal * _OutlineThickness;
				float3 position = v.vertex + outlineOffset;
				o.position = UnityObjectToClipPos(position);

				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET
			{
				return _OutlineColor;
			}

		ENDCG
		}
	}
}