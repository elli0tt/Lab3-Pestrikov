using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace Lab3 {
    class View {
        int BasicProgramID = 0, BasicVertexShader = 0, BasicFragmentShader = 0;
        int vbo_position = 0;
        private int attribute_vpos = 0;
        int uniform_pos = 0;
        int uniform_aspect = 0;
        int aspect = 0;

        Vector3 campos;
        void loadShader(String filename, ShaderType type, int program, out int address) {
            address = GL.CreateShader(type);
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename)) {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        public void InitShaders() {
            BasicProgramID = GL.CreateProgram();
            loadShader("../../raytracing.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            loadShader("../../raytracing.frag", ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);
            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
        }

        public void Update() {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            InitBuffers();
        }
        public void InitBuffers() {
            Vector3[] vertdata = new Vector3[]
            {
                new Vector3(-1f, -1f, 0f),
                new Vector3( 1f, -1f, 0f),
                new Vector3( 1f,  1f, 0f),
                new Vector3(-1f,  1f, 0f)
            };
            GL.GenBuffers(1, out vbo_position);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(attribute_vpos);
            GL.Uniform3(uniform_pos, campos);
            GL.Uniform1(uniform_aspect, aspect);

            GL.UseProgram(BasicProgramID);
            GL.DrawArrays(BeginMode.Quads, 0, 4);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
