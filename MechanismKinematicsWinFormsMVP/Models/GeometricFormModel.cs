using System;

namespace MechanismKinematicsWinFormsMVP
{
    public class GeometricFormModel
    {
        public int RadiusOne { get; set; }
        public int RadiusTwo { get; set; }

        public string GetLabelRadiusOneText()
        {
            return Convert.ToString(RadiusOne) + " inches";
        }

        public string GetLabelRadiusTwoText()
        {
            return Convert.ToString(RadiusTwo) + " inches";
        }
    }
}
