Shader "Render Depth" {
	SubShader{
		Tags { "RenderType" = "Opaque" }
		//Tags { "LightMode" = "ShadowCaster" }
		Pass {
		//Tags { "LightMode" = "ShadowCaster" }
		
		CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"



			struct v2f {
				float4 pos : SV_POSITION;
				float2 depth : TEXCOORD0;
			};

			v2f vert(appdata_base v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_DEPTH(o.depth);
				return o;
			}

			
			half4 frag(v2f i) : SV_Target {
				//UNITY_OUTPUT_DEPTH(i.depth);
				//UNITY_OUTPUT_DEPTH(i.depth*100000);

				//fixed4 col = float4(1,0.7,0,0.2);
				//return col;

				return float4(i.depth,0,1);
			}
			ENDCG
			

			/*
			//the fragment shader
			fixed4 frag(v2f i) : SV_TARGET{
				//get depth from depth texture
				float depth = tex2D(_CameraDepthTexture, i.uv).r;
			//linear depth between camera and far clipping plane
			depth = Linear01Depth(depth);

			return depth;
			}
			*/
			//ENDCG
		}
	}
}