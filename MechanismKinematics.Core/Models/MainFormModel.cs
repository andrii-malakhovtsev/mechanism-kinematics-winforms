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
        private readonly MechanismPainter _mechanismPainter;
        private double _omega = 0.5;
        private double _time = 0;
        private int _radiusOne = 80;
        private int _radiusTwo = 120;

        public MainFormModel()
        {
            _mechanismPainter = new MechanismPainter(this);
            _mechanismController = new MechanismController(this, _mechanismPainter);
            _mechanismPainter.MechanismController = _mechanismController;
        }

        public Point Center { get; private set; }

        public Point PictureBoxLocation => new Point(0, MenuStripHeight);

        public Graphics Graphics { get; set; } = null;

        public string LabelRadiusOneText => GetLabelRadiusText(RadiusOne);

        public string LabelRadiusTwoText => GetLabelRadiusText(RadiusTwo);

        public string LabelOmegaText => Convert.ToString(Omega) + " rad/s";

        public int RadiusOne 
        { 
            get => _radiusOne; 
            set => MechanismController.SetRadius(ref _radiusOne, value); 
        }

        public int RadiusTwo 
        { 
            get => _radiusTwo;
            set => MechanismController.SetRadius(ref _radiusTwo, value);
        }

        public int ClientSizeHeight { private get; set; }

        public int PictureBoxWidth { get; set; }

        public int PictureBoxHeight => ClientSizeHeight - MenuStripHeight - PanelHeight;

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

        public double Time
        {
            get => _time; 
            private set 
            { 
                if (value >= 0) 
                    _time = value; 
            }
        }

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
            => Convert.ToString(radius) + " inches";

        public void SetCenterCooridnates() 
            => Center = new Point(PictureBoxWidth / 2, PictureBoxHeight / 2);

        public void ResetTime() => Time = 0;

        public void TimerTick()
        {
            _mechanismPainter.RefreshPicture(clear: false, clearStable: false);
            Time += FrameInterval;
            _mechanismPainter.RefreshPicture(clear: true, clearStable: false);
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
                _mechanismPainter.RefreshPicture(clear: true, clearStable: false);
        }

        public void DrawMechanismAfterFormClose()
        {
            if (!DrawToolStripEnabled) 
                DrawMechanism();
        }

        public void DrawMechanism()
        {
            ResetTime();
            _mechanismPainter.RefreshPicture(clear: true, clearStable: false);
            SetPictureMechanismEnables(draw: true);
            MechanismDrawn = true;
            _mechanismController.ClearPointsLists();
        }

        public void ClearMechanism()
        {
            _mechanismPainter.RefreshPicture(clear: false, clearStable: true);
            _mechanismPainter.ClearDrawing();
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
            => SetMechanismAnimationEnables(startPictureAnimation: false);

        public void StartMechanismAnimation()
        {
            _mechanismPainter.ClearDrawing();
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