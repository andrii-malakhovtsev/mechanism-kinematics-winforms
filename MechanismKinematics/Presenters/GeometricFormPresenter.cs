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

        public string GetLabelRadiusOneText()
        {
            return _model.GetLabelRadiusOneText();
        }

        public string GetLabelRadiusTwoText()
        {
            return _model.GetLabelRadiusTwoText();
        }

        public int GetRadiusOne()
        {
            return _model.RadiusOne;
        }

        public int GetRadiusTwo()
        {
            return _model.RadiusTwo;
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            _model.RadiusOne = _view.RadiusOne;
            _model.RadiusTwo = _view.RadiusTwo;
        }
    }
}