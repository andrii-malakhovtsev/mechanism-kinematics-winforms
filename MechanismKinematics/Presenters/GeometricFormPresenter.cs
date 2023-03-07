using System;

namespace MechanismKinematics
{
    public class GeometricFormPresenter
    {
        private readonly GeometricFormModel _model = new GeometricFormModel();
        private readonly IGeometricFormView _view;

        public GeometricFormPresenter(IGeometricFormView view)
        {
            _view = view;
            _view.GeometricFormLoad += new EventHandler<EventArgs>(OnGeometricFormLoad);
            _view.ButtonOKClick += new EventHandler<EventArgs>(OnOKClick);
        }

        public string LabelRadiusOneText { get => _model.LabelRadiusOneText; }

        public string LabelRadiusTwoText { get => _model.LabelRadiusTwoText; }

        public int RadiusOne { get => _model.RadiusOne; }

        public int RadiusTwo { get => _model.RadiusTwo; }

        public void SetParameters(int radiusOne, int radiusTwo)
        {
            _model.RadiusOne = radiusOne;
            _model.RadiusTwo = radiusTwo;
        }

        private void OnGeometricFormLoad(object sender, EventArgs e)
        {
            _view.RadiusOne = _model.RadiusOne;
            _view.RadiusTwo = _model.RadiusTwo;
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            _model.RadiusOne = _view.RadiusOne;
            _model.RadiusTwo = _view.RadiusTwo;
        }
    }
}