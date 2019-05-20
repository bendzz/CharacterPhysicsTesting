Shader "Custom/UAVRenderer"
{
	Properties
	{
	}
	SubShader
	{
		Pass
		{
			Cull Off ZWrite Off ZTest Always Fog{ Mode Off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 5.0

			#include "UnityCG.cginc"

			struct	vin
			{
				float x;
				float y;
			};

			StructuredBuffer<vin>		vdata;
			RWStructuredBuffer<float>	Field	: register(u1);

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 id : COLOR0;
			};
			
			v2f vert (uint id : SV_VertexID, uint inst : SV_InstanceID)
			{
				v2f o;
				vin v = vdata[id];
				o.vertex = float4(v.x,v.y,id,1);
				o.id.x = id;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				Field[1] = 106.445f;
				Field[50] = 12.89f;
				Field[100] = 26.5f;
				Field[999] = -100.0f;
				return 1;
			}
			ENDCG
		}
	}
}
