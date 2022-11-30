using System;
using System.Drawing;

namespace MechanismKinematics
{
    public class MainFormModel
    {
        private static readonly double s_frameInterval = 0.1;
        private readonly MechanismController _mechanismController;
        public Point Center { get; set; }
        public Graphics Graphics { get; set; } = null;
        public int RadiusOne { get; set; } = 80;
        public int RadiusTwo { get; set; } = 120;
        public int TimerInterval { get; set; } = 20;
        public int ClientSizeHeight { get; set; }
        public int ClientSizeWidth { get; set; }
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

        public Point GetPictureBoxLocation()
        {
            return new Point(0, MenuStripHeight);
        }

        public int GetPictureBoxWidth()
        {
            return ClientSizeWidth;
        }

        public int GetPictureBoxHeight()
        {
            return ClientSizeHeight - MenuStripHeight - PanelHeight;
        }

        public void SetCenterCoordinates()
        {
            Center = new Point(GetPictureBoxWidth() / 2, GetPictureBoxHeight() / 2);
        }

        public string GetLabelRadiusOneText()
        {
            return GetLabelRadiusText(isRadiusOne: true) ;
        }

        public string GetLabelRadiusTwoText()
        {
            return GetLabelRadiusText(isRadiusOne: false);
        }

        private string GetLabelRadiusText(bool isRadiusOne)
        {
            return Convert.ToString(isRadiusOne ? RadiusOne : RadiusTwo) + " inches";
        }

        public string GetLabelOmegaText()
        {
            return Convert.ToString(Omega) + " rad/s";
        }

        public int GetTimerInterval()
        {
            return (int)(TimerInterval / Omega);
        }

        public void ResetTime()
        {
            Time = 0;
        }

        public void TimerTick()
        {
            _mechanismController.RefreshFields();
            _mechanismController.RefreshPicture(clear: false, clearStable: false);
            Time += s_frameInterval;
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