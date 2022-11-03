using System;

namespace MechanismKinematicsWinFormsMVP
{
    public interface IKinematicFormView
    {
        string MaskedTextBoxText { get; set; }
        event EventHandler<EventArgs> ButtonOkClick;
    }
}