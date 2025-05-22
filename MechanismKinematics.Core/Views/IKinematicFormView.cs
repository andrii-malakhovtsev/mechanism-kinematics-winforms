using System;

namespace MechanismKinematics
{
    public interface IKinematicFormView
    {
        string MaskedTextBoxText { get; set; }
        event EventHandler<EventArgs> ButtonOkClick;
    }
}