using System;
using System.Windows.Forms;

namespace Lab3 {
    public partial class Form1 : Form {
        View view = new View();

        public Form1() {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e) {
            view.InitShaders();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e) {
            view.Update();
            glControl1.SwapBuffers();
        }
    }
}
