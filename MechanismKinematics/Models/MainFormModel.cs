using System;
using System.Drawing;

namespace MechanismKinematics
{
    public class MainFormModel
    {
        private const double FrameInterval = 0.1;
        private const int MaxOmegaValue = 100;
        public const int TimerInterval = 20;
        private readonly MechanismController _mechanismController;
        private double _omega = 0.5;
        private double _time = 0;
        private int _radiusOne = 80;
        private int _radiusTwo = 120;

        public MainFormModel()
        {
            _mechanismController = new MechanismController(this);
        }

        public Point Center { get; set; }
        public Point PictureBoxLocation { get => new Point(0, MenuStripHeight); }
        public Graphics Graphics { get; set; } = null;
        public string LabelRadiusOneText { get => GetLabelRadiusText(RadiusOne); }
        public string LabelRadiusTwoText { get => GetLabelRadiusText(RadiusTwo); }
        public string LabelOmegaText { get => Convert.ToString(Omega) + " rad/s"; }
        public int RadiusOne { get => _radiusOne; set { if (value >= 0) _radiusOne = value; } }
        public int RadiusTwo { get => _radiusTwo; set { if (value >= 0) _radiusTwo = value; } }
        public int ClientSizeHeight { private get; set; }
        public int PictureBoxWidth { get; set; }
        public int PictureBoxHeight { get => ClientSizeHeight - MenuStripHeight - PanelHeight; }
        public int PanelHeight { private get; set; }
        public int MenuStripHeight { private get; set; }
        public double Omega 
        {
            get => _omega; 
            set 
            { 
                if (value >= -MaxOmegaValue && value <= MaxOmegaValue)
                    _omega = value; 
            } 
        }
        public double Time { get { return _time; } private set { if (value >= 0) _time = value; } }
        public bool MechanismDrawn { get; set; } = false;
        public bool TimerEnabled { get; private set; } = true;
        public bool ClearToolStripEnabled { get; private set; } = false;
        public bool StartToolStripEnabled { get; private set; } = false;
        public bool StopToolStripEnabled { get; private set; } = false;
        public bool DrawToolStripEnabled { get; set; } = true;
        public bool PointAEnabled { get; private set; } = false;
        public bool PointBEnabled { get; private set; } = false;
        public bool GeometricToolStripEnabled { get; private set; }
        public bool KinematicToolStripEnabled { get; private set; }
        public bool PointAChecked { get; set; }
        public bool PointBChecked { get; set; }

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

        public void DrawMechanismAfterFormClose()
        {
            if (!DrawToolStripEnabled) 
                DrawMechanism();
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