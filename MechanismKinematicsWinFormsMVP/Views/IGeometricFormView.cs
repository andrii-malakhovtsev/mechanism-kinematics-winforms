using System;

namespace MechanismKinematicsWinFormsMVP
{
    public interface IGeometricFormView
    {
        int RadiusOne { get; set; }
        int RadiusTwo { get; set; }
        event EventHandler<EventArgs> GeometricFormLoad;
        event EventHandler<EventArgs> ButtonOKClick;
    }
}