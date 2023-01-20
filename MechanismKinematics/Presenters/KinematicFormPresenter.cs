using System;

namespace MechanismKinematics
{
    public class KinematicFormPresenter
    {
        private readonly KinematicFormModel _model = new KinematicFormModel();
        private readonly IKinematicFormView _view;
        public double Omega { get => _model.Omega; }

        public KinematicFormPresenter(IKinematicFormView View)
        {
            _view = View;
            _view.ButtonOkClick += new EventHandler<EventArgs>(OnbuttonOkClick);
        }

        private void OnbuttonOkClick(object sender, EventArgs e)
        {
            _model.OmegaFullString = _view.MaskedTextBoxText;
        }
    }
}