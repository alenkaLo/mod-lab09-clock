using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
namespace _9.Clock
{
    public partial class Form1 : Form
    {
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        DateTime dt = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            Pen cir_pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Indigo);
            Graphics g = pictureBox1.CreateGraphics();
            GraphicsState gs;
            
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g.ScaleTransform(pictureBox1.Height / 200, pictureBox1.Height / 200);
            //g.DrawEllipse(cir_pen, -100, -100, 190, 190);
            int size = 141;
            g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.White), new Rectangle(-size/2, -size/2, size, size));
            
            gs = g.Save();
            g.RotateTransform(6 * (dt.Hour + (float)(dt.Minute + (float)dt.Second / 60) / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Brown), 4), 0, 0, 0, -60);
            g.Restore(gs);
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 3), 0, 0, 0, -65);
            g.Restore(gs);
            gs = g.Save();
            g.RotateTransform(6 * (float)dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 1), 0, 0, 0, -70);
            g.Restore(gs);

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now - dt > TimeSpan.FromSeconds(1)) { dt = DateTime.Now; Invalidate(); }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            const uint WM_SYSCOMMAND = 0x0112;
            const uint DOMOVE = 0xF012;

            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }
    }
}
