using System;

namespace MechanismKinematicsWinFormsMVP
{
    public class KinematicFormPresenter
    {
        private readonly KinematicFormModel _model = new KinematicFormModel();
        private readonly IKinematicFormView _view;

        public KinematicFormPresenter(IKinematicFormView View)
        {
            _view = View;
            _view.ButtonOkClick += new EventHandler<EventArgs>(OnbuttonOkClick);
        }

        public double GetOmega()
        {
            return _model.GetOmega();
        }

        private void OnbuttonOkClick(object sender, EventArgs e)
        {
            _model.OmegaFullString = _view.MaskedTextBoxText;
        }
    }
}
