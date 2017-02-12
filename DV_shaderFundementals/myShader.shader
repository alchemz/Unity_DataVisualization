Shader "Custom/myShader"{
  SubShader{
    Pass{
          CGPROHRAM
          
          #pragma vertex MyVertexProgram
          #pragma fragment myFragmentProgram
          #include "UnityCG.cginc"
          float4 MyVertexProgram(float4 position:POSITION): SV_POSITION{
            
          }
          
          float4 MyFragmentProgram(float4 position:POSITION): SV_TARGET{
          
          }
          ENDCG
    }
  }
}
