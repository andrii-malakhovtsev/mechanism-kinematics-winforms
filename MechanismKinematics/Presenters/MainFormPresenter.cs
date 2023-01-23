using System;

namespace MechanismKinematics
{
    public class MainFormPresenter
    {
        private readonly MainFormModel _model = new MainFormModel();
        private readonly IMainFormView _view;
        public event EventHandler<EventArgs> MakeNewGeometricForm;
        public event EventHandler<EventArgs> MakeNewKinematicForm;

        public MainFormPresenter(IMainFormView view)
        {
            _view = view;
            _view.AppOpen += new EventHandler<EventArgs>(OnAppOpen);
            _view.DrawMechanism += new EventHandler<EventArgs>(OnDrawMechanism);
            _view.GraphicsSetup += new EventHandler<EventArgs>(OnGraphicsSetup);
            _view.TimerTick += new EventHandler<EventArgs>(OnTimerTick);
            _view.StartMechanismAnimation += new EventHandler<EventArgs>(OnStartMechanismAnimation);
            _view.StopMechanismAnimation += new EventHandler<EventArgs>(OnStopMechanismAnimation);
            _view.ClearMechanism += new EventHandler<EventArgs>(OnClearMechanism);
            _view.PictureBoxPaint += new EventHandler<EventArgs>(OnPictureBoxPaint);
            _view.PointAMenuStripClick += new EventHandler<EventArgs>(OnPointAMenuStripClick);
            _view.PointBMenuStripClick += new EventHandler<EventArgs>(OnPointBMenuStripClick);
            _view.GeometricFormOpen += new EventHandler<EventArgs>(OnGeometricFormOpen);
            _view.KinematicFormOpen += new EventHandler<EventArgs>(OnKinematicFormOpen);
            SetView();
        }

        public int RadiusOne { get => _model.RadiusOne; }
        public int RadiusTwo { get => _model.RadiusTwo; }

        private void OnKinematicFormOpen(object sender, EventArgs e)
        {
            OnFormOpen(MakeNewKinematicForm);
        }

        private void OnGeometricFormOpen(object sender, EventArgs e)
        {
            OnFormOpen(MakeNewGeometricForm);
        }

        private void OnFormOpen(EventHandler<EventArgs> @event)
        {
            _model.ClearMechanism();
            @event?.Invoke(this, EventArgs.Empty);
            _view.LabelOmegaText = _model.LabelOmegaText;
            _model.DrawToolStripEnabled = _view.DrawToolStripEnabled; 
            _model.DrawMechanismAfterForm();
        }

        private void OnPointAMenuStripClick(object sender, EventArgs e)
        {
            _model.PointACheck();
            _view.PointAChecked = _model.PointAChecked;
        }

        private void OnPointBMenuStripClick(object sender, EventArgs e)
        {
            _model.PointBCheck();
            _view.PointBChecked = _model.PointBChecked;
        }

        private void OnPictureBoxPaint(object sender, EventArgs e)
        {
            _model.PictureBoxPaint();
        }

        private void OnClearMechanism(object sender, EventArgs e)
        {
            _model.ClearMechanism();
            RefreshView();
            _view.DrawToolStripEnabled = _model.DrawToolStripEnabled;
            _view.PointAEnabled = _model.PointAEnabled;
            _view.PointBEnabled = _model.PointBEnabled;
        }

        private void OnStopMechanismAnimation(object sender, EventArgs e)
        {
            _model.StopMechanismAnimation();
            RefreshView();
        }

        private void OnStartMechanismAnimation(object sender, EventArgs e)
        {
            _model.StartMechanismAnimation();
            RefreshView();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _model.PointAChecked = _view.PointAChecked;
            _model.PointBChecked = _view.PointBChecked;
            _model.TimerTick();
        }

        private void OnGraphicsSetup(object sender, EventArgs e)
        {
            _model.Graphics = _view.Graphics;
        }

        private void OnDrawMechanism(object sender, EventArgs e)
        {
            _model.ResetTime();
            _view.Time = _model.Time;
            _model.DrawMechanism();
            RefreshView();
            _view.DrawToolStripEnabled = _model.DrawToolStripEnabled;
            _view.Drawn = _model.MechanismDrawn;
            _view.PointAEnabled = _model.PointAEnabled;
            _view.PointBEnabled = _model.PointBEnabled;
        }

        private void OnAppOpen(object sender, EventArgs e)
        {
            _model.MenuStripHeight = _view.MenuStripHeight;
            _model.PictureBoxWidth = _view.ClientSizeWidth;
            _model.ClientSizeHeight = _view.ClientSizeHeight;
            _model.MenuStripHeight = _view.MenuStripHeight;
            _model.PanelHeight = _view.PanelHeight;
            _view.PictureBoxLocation = _model.PictureBoxLocation;
            _view.PictureBoxHeight = _model.PictureBoxHeight;
            _view.PictureBoxWidth = _model.PictureBoxWidth;
            _model.SetCenterCooridnates();
            _model.Graphics = _view.Graphics;
            RefreshView();
            _view.LabelRadiusOneText = _model.LabelRadiusOneText;
            _view.LabelRadiusTwoText = _model.LabelRadiusTwoText;
            _view.LabelOmegaText = _model.LabelOmegaText;
            _view.TimerInterval = MainFormModel.TimerInterval;
        }

        private void SetView()
        {
            _view.Omega = _model.Omega;
            _view.RadiusOne = _model.RadiusOne;
            _view.RadiusTwo = _model.RadiusTwo;
            _view.Center = _model.Center;
            _view.Drawn = _model.MechanismDrawn;
            _view.Time = _model.Time;
        }

        private void RefreshView()
        {
            _view.ClearToolStripEnabled = _model.ClearToolStripEnabled;
            _view.StartToolStripEnabled = _model.StartToolStripEnabled;
            _view.StopToolStripEnabled = _model.StopToolStripEnabled;
            _view.TimerEnabled = _model.TimerEnabled;
            _view.GeometricToolStripEnabled = _model.GeometricToolStripEnabled;
            _view.KinematicToolStripEnabled = _model.KinematicToolStripEnabled;
        }

        public void SetRadiusesText(string radiusOneText, string radiusTwoText)
        {
            _view.LabelRadiusOneText = radiusOneText;
            _view.LabelRadiusTwoText = radiusTwoText;
        }

        public void SetRadiuses(int radiusOne, int radiusTwo)
        {
            _model.RadiusOne = radiusOne;
            _model.RadiusTwo = radiusTwo;
            _view.RadiusOne = _model.RadiusOne;
            _view.RadiusTwo = _model.RadiusTwo;
        }

        public void SetOmega(double omega)
        {
            _model.Omega = omega;
            _view.Omega = _model.Omega;
        }
    }
}