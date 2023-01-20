using System;
using System.Drawing;

namespace MechanismKinematics
{
    public class MainFormModel
    {
        private const double FrameInterval = 0.1;
        public const int TimerInterval = 20;
        private readonly MechanismController _mechanismController;
        public Point Center { get; set; }
        public Point PictureBoxLocation { get => new Point(0, MenuStripHeight); }
        public Graphics Graphics { get; set; } = null;
        public string LabelRadiusOneText { get => GetLabelRadiusText(RadiusOne); }
        public string LabelRadiusTwoText { get => GetLabelRadiusText(RadiusTwo); }
        public string LabelOmegaText { get => Convert.ToString(Omega) + " rad/s"; }
        public int RadiusOne { get; set; } = 80;
        public int RadiusTwo { get; set; } = 120;
        public int ClientSizeHeight { get; set; }
        public int PictureBoxWidth { get; set; }
        public int PictureBoxHeight { get => ClientSizeHeight - MenuStripHeight - PanelHeight; }
        public int PanelHeight { get; set; }
        public int MenuStripHeight { get; set; }
        public double Omega { get; set; } = 0.5;
        public double Time { get; set; } = 0;
        public bool MechanismDrawn { get; set; } = false;
        public bool TimerEnabled { get; set; } = true;
        public bool ClearToolStripEnabled { get; set; } = false;
        public bool StartToolStripEnabled { get; set; } = false;
        public bool StopToolStripEnabled { get; set; } = false;
        public bool DrawToolStripEnabled { get; set; } = true;
        public bool PointAEnabled { get; set; } = false;
        public bool PointBEnabled { get; set; } = false;
        public bool GeometricToolStripEnabled { get; set; }
        public bool KinematicToolStripEnabled { get; set; }
        public bool PointAChecked { get; set; }
        public bool PointBChecked { get; set; }

        public MainFormModel()
        {
            _mechanismController = new MechanismController(this);
        }

        private string GetLabelRadiusText(int radius)
        {
            return Convert.ToString(radius) + " inches";
        }

        public void SetCenterCooridnates()
        {
            Center = new Point(PictureBoxWidth / 2, PictureBoxHeight / 2);
        }

        public void ResetTime()
        {
            Time = 0;
        }

        public void TimerTick()
        {
            _mechanismController.RefreshFields();
            _mechanismController.RefreshPicture(clear: false, clearStable: false);
            Time += FrameInterval;
            _mechanismController.RefreshPicture(clear: true, clearStable: false);
            _mechanismController.CountTrajectory();
        }

        public void PointACheck()
        {
            PointAChecked = !PointAChecked;
            _mechanismController.PointCheck();
        }

        public void PointBCheck()
        {
            PointBChecked = !PointBChecked;
            _mechanismController.PointCheck();
        }

        public void PictureBoxPaint()
        {
            if (MechanismDrawn)
                _mechanismController.RefreshPicture(clear: true, clearStable: false);
        }

        public void DrawMechanismAfterForm()
        {
            if (!DrawToolStripEnabled) DrawMechanism();
        }

        public void DrawMechanism()
        {
            ResetTime();
            _mechanismController.RefreshPicture(clear: true, clearStable: false);
            SetPictureMechanismEnables(draw: true);
            MechanismDrawn = true;
            _mechanismController.ClearPointsLists();
        }

        public void ClearMechanism()
        {
            _mechanismController.RefreshPicture(clear: false, clearStable: true);
            _mechanismController.ClearDrawing();
            SetPictureMechanismEnables(draw: false);
        }

        private void SetPictureMechanismEnables(bool draw)
        {
            DrawToolStripEnabled = !draw;
            ClearToolStripEnabled = draw;
            StartToolStripEnabled = draw;
            StopToolStripEnabled = false;
            PointAEnabled = draw;
            PointBEnabled = draw;
        }

        public void StopMechanismAnimation()
        {
            SetMechanismAnimationEnables(startPictureAnimation: false);
        }

        public void StartMechanismAnimation()
        {
            _mechanismController.ClearDrawing();
            SetMechanismAnimationEnables(startPictureAnimation: true);
            _mechanismController.AddPointsToLists();
        }

        private void SetMechanismAnimationEnables(bool startPictureAnimation)
        {
            TimerEnabled = startPictureAnimation;
            StopToolStripEnabled = startPictureAnimation;
            StartToolStripEnabled = !startPictureAnimation;
            ClearToolStripEnabled = !startPictureAnimation;
            GeometricToolStripEnabled = !startPictureAnimation;
            KinematicToolStripEnabled = !startPictureAnimation;
        }
    }
}