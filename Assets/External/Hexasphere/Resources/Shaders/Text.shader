﻿Shader "Hexasphere/Text" {
	Properties {
		_MainTex ("Font Texture", 2D) = "white" {}
		_Color ("Text Color", Color) = (1,1,1,1)
	}


		SubShader{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }
			Lighting Off Cull Off Fog { Mode Off }
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			Pass {
				 CGPROGRAM
				 #pragma vertex vert
				 #pragma fragment frag
				 #pragma fragmentoption ARB_precision_hint_fastest

				 #include "UnityCG.cginc"

				 struct appdata_t {
					 float4 vertex : POSITION;
					 fixed4 color : COLOR;
					 float2 texcoord : TEXCOORD0;
					 UNITY_VERTEX_INPUT_INSTANCE_ID
				 };

				 struct v2f {
					 float4 vertex : POSITION;
					 fixed4 color : COLOR;
					 float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_OUTPUT_STEREO
				 };

				 sampler2D _MainTex;
				 uniform float4 _MainTex_ST;
				 uniform fixed4 _Color;

				 v2f vert(appdata_t v)
				 {
					 v2f o;
	                 UNITY_SETUP_INSTANCE_ID(v);
					 UNITY_INITIALIZE_OUTPUT(v2f, o);
					 UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					 o.vertex = UnityObjectToClipPos(v.vertex);
					 #if UNITY_REVERSED_Z
						 o.vertex.z += 0.001;
					 #else
						 o.vertex.z -= 0.001;
					 #endif  
					 o.color = v.color * _Color;
					 o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					 return o;
				 }

				 fixed4 frag(v2f i) : SV_Target
				 {
					 fixed4 col = i.color;
					 col.a *= UNITY_SAMPLE_1CHANNEL(_MainTex, i.texcoord);
					 return col;
				 }
				 ENDCG
			 }
		}

	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off Cull Off Fog { Mode Off }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform fixed4 _Color;
			
			v2f vert (appdata_t v)
			{
				v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.vertex = UnityObjectToClipPos(v.vertex);
				#if UNITY_REVERSED_Z
					o.vertex.z += 0.001;
				#else
					o.vertex.z -= 0.001;
				#endif	
				o.color = v.color * _Color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = i.color;
				col.a *= UNITY_SAMPLE_1CHANNEL(_MainTex, i.texcoord);
				return col;
			}
			ENDCG 
		}
	} 	

	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off Cull Off Fog { Mode Off }
		ZWrite Off
		Offset -2, -2
		Blend SrcAlpha OneMinusSrcAlpha
		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}
		Pass {
			SetTexture [_MainTex] { 
				constantColor [_Color] combine constant * primary, constant * texture
			}
		}
	}


}