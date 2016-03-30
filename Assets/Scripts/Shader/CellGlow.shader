Shader "Custom/CellGlow" {
Properties
{
    _Color ("Main Color", Color) = (1,1,1,1)
}
SubShader {
    Tags {"Queue"="Transparent"}
    LOD 200
	    
    Pass
	{
       ZWrite On
       ColorMask 0
    }

   
    UsePass "Transparent/Diffuse/FORWARD"
}
Fallback "Transparent/VertexLit"
}
