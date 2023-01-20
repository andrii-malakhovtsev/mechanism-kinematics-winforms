using System;

namespace MechanismKinematics
{
    public class MainInteractions
    {
        private readonly MainFormPresenter _mainFormPresenter;

        public MainInteractions(MainForm mainView)
        {
            MainFormModel mainFormModel = new MainFormModel();
            MainFormPresenter mainFormPresenter = new MainFormPresenter(mainView);
            _mainFormPresenter = mainFormPresenter;
            mainFormPresenter.MakeNewGeometricForm += new EventHandler<EventArgs>(OnMakeNewGeometricForm);
            mainFormPresenter.MakeNewKinematicForm += new EventHandler<EventArgs>(OnMakeNewKinematicForm);
        }

        public void OnMakeNewGeometricForm(object sender, EventArgs e)
        {
            GeometricForm geometricForm = new GeometricForm();
            GeometricFormPresenter geometricFormPresenter = new GeometricFormPresenter(geometricForm);
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
            KinematicForm kinematicForm = new KinematicForm();
            KinematicFormPresenter kinematicFormPresenter = new KinematicFormPresenter(kinematicForm);
            kinematicForm.ShowDialog();
            _mainFormPresenter.SetOmega(kinematicFormPresenter.Omega);
        }
    }
}