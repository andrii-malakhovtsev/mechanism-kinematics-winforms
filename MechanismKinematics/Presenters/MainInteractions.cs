using System;

namespace MechanismKinematics
{
    public class MainInteractions
    {
        private readonly MainFormPresenter _mainFormPresenter;

        public MainInteractions(MainForm mainView)
        {
            var mainFormModel = new MainFormModel();
            _mainFormPresenter = new MainFormPresenter(mainView);
            _mainFormPresenter.MakeNewGeometricForm += new EventHandler<EventArgs>(OnMakeNewGeometricForm);
            _mainFormPresenter.MakeNewKinematicForm += new EventHandler<EventArgs>(OnMakeNewKinematicForm);
        }

        public void OnMakeNewGeometricForm(object sender, EventArgs e)
        {
            var geometricForm = new GeometricForm();
            var geometricFormPresenter = new GeometricFormPresenter(geometricForm);
            geometricFormPresenter.SetParameters(_mainFormPresenter.RadiusOne, 
                _mainFormPresenter.RadiusTwo);
            geometricForm.ShowDialog();
            _mainFormPresenter.SetRadiuses(geometricFormPresenter.RadiusOne,
                geometricFormPresenter.RadiusTwo);
            _mainFormPresenter.SetRadiusesText(geometricFormPresenter.LabelRadiusOneText, 
                geometricFormPresenter.LabelRadiusTwoText);
        }

        public void OnMakeNewKinematicForm(object sender, EventArgs e)
        {
            var kinematicForm = new KinematicForm();
            var kinematicFormPresenter = new KinematicFormPresenter(kinematicForm);
            kinematicForm.ShowDialog();
            _mainFormPresenter.SetOmega(kinematicFormPresenter.Omega);
        }
    }
}