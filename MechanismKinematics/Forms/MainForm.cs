using System;
using System.Drawing;
using System.Windows.Forms;

namespace MechanismKinematics
{
    public partial class MainForm : Form, IMainFormView
    {
        #region Interface implementation

        public Graphics Graphics { get; set; }
        public Point Center { get; set; }
        public Point PictureBoxLocation { get; set; }
        public double Omega { get; set; }
        public double Time { get; set; }
        public int RadiusOne { get; set; }
        public int RadiusTwo { get; set; }
        public int PictureBoxWidth { get; set; }
        public int PictureBoxHeight { get; set; }
        public int ClientSizeHeight { get; set; }
        public int ClientSizeWidth { get; set; }
        public int PanelHeight { get; set; }
        public int MenuStripHeight { get; set; }
        public int TimerInterval { get; set; }
        public string LabelRadiusOneText { get; set; }
        public string LabelRadiusTwoText { get; set; }
        public string LabelOmegaText { get; set; }
        public bool Drawn { get; set; }
        public bool ClearToolStripEnabled { get; set; }
        public bool StartToolStripEnabled { get; set; }
        public bool StopToolStripEnabled { get; set; }
        public bool DrawToolStripEnabled { get; set; }
        public bool KinematicToolStripEnabled { get; set; }
        public bool GeometricToolStripEnabled { get; set; }
        public bool TimerEnabled { get; set; }
        public bool PointAChecked { get; set; }
        public bool PointBChecked { get; set; }
        public bool PointAEnabled { get; set; }
        public bool PointBEnabled { get; set; }
        public event EventHandler<EventArgs> AppOpen;
        public event EventHandler<EventArgs> GraphicsSetup;
        public event EventHandler<EventArgs> DrawMechanism;
        public event EventHandler<EventArgs> StartMechanismAnimation;
        public event EventHandler<EventArgs> StopMechanismAnimation;
        public event EventHandler<EventArgs> ClearMechanism;
        public event EventHandler<EventArgs> PictureBoxPaint;
        public event EventHandler<EventArgs> TimerTick;
        public event EventHandler<EventArgs> PointAMenuStripClick;
        public event EventHandler<EventArgs> PointBMenuStripClick;
        public event EventHandler<EventArgs> GeometricFormOpen;
        public event EventHandler<EventArgs> KinematicFormOpen;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MenuStripHeight = menuStrip.ClientSize.Height;
            ClientSizeHeight = ClientSize.Height;
            ClientSizeWidth = ClientSize.Width;
            PanelHeight = ParametersPanel.Height;
            MenuStripHeight = menuStrip.ClientSize.Height;
            AppOpen?.Invoke(this, EventArgs.Empty);
            pictureBox.Location = PictureBoxLocation;
            pictureBox.Width = PictureBoxWidth;
            pictureBox.Height = PictureBoxHeight;
            Graphics = pictureBox.CreateGraphics();
            GraphicsSetup?.Invoke(this, EventArgs.Empty);
            LabelRadiusOne.Text = LabelRadiusOneText;
            LabelRadiusTwo.Text = LabelRadiusTwoText;
            LabelOmega.Text = LabelOmegaText;
            Timer.Interval = TimerInterval;
            ClearToolStripMenuItem.Enabled = ClearToolStripEnabled;
            SetStartStopToolStripItemEnables();
        }

        private void SetStartStopToolStripItemEnables()
        {
            StartToolStripMenuItem.Enabled = StartToolStripEnabled;
            StopToolStripMenuItem.Enabled = StopToolStripEnabled;
        }

        private void DrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawMechanism?.Invoke(this, EventArgs.Empty);
            SetMenuItemsEnables();
            SetStartStopToolStripItemEnables();
        }

        private void SetMenuItemsEnables()
        {
            PointAToolStripMenuItem.Enabled = PointAEnabled;
            PointBToolStripMenuItem.Enabled = PointBEnabled;
            DrawToolStripMenuItem.Enabled = DrawToolStripEnabled;
            ClearToolStripMenuItem.Enabled = ClearToolStripEnabled;
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearMechanism?.Invoke(this, EventArgs.Empty);
            SetMenuItemsEnables();
            StartToolStripMenuItem.Enabled = StartToolStripEnabled;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics.Dispose();
            Close();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartMechanismAnimation?.Invoke(this, EventArgs.Empty);
            SetStartStopMenuEnables();
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopMechanismAnimation?.Invoke(this, EventArgs.Empty);
            SetStartStopMenuEnables();
        }

        private void SetStartStopMenuEnables()
        {
            Timer.Enabled = TimerEnabled;
            SetStartStopToolStripItemEnables();
            ClearToolStripMenuItem.Enabled = ClearToolStripEnabled;
            GeometricToolStripMenuItem.Enabled = GeometricToolStripEnabled;
            KinematicToolStripMenuItem.Enabled = KinematicToolStripEnabled;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            PointAChecked = PointAToolStripMenuItem.Checked;
            PointBChecked = PointBToolStripMenuItem.Checked;
            TimerTick?.Invoke(this, EventArgs.Empty);
        }

        private void GeometricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawToolStripEnabled = DrawToolStripMenuItem.Enabled;
            GeometricFormOpen?.Invoke(this, EventArgs.Empty);
            LabelRadiusOne.Text = LabelRadiusOneText;
            LabelRadiusTwo.Text = LabelRadiusTwoText;
        }

        private void KinematicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawToolStripEnabled = DrawToolStripMenuItem.Enabled;
            KinematicFormOpen?.Invoke(this, EventArgs.Empty);
            LabelOmega.Text = LabelOmegaText;
        }

        private void PointAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PointAMenuStripClick?.Invoke(this, EventArgs.Empty);
            PointAToolStripMenuItem.Checked = PointAChecked;
        }

        private void PointBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PointBMenuStripClick?.Invoke(this, EventArgs.Empty);
            PointBToolStripMenuItem.Checked = PointBChecked;
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBoxPaint?.Invoke(this, EventArgs.Empty);
        }
    }
}