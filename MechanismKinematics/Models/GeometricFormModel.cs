using System;

namespace MechanismKinematics
{
    public class GeometricFormModel
    {
        private int _radiusOne;
        private int _radiusTwo;

        public int RadiusOne { get { return _radiusOne; } set { if (value >= 0) _radiusOne = value; } }
        public int RadiusTwo { get { return _radiusTwo; } set { if (value >= 0) _radiusTwo = value; } }
        public string LabelRadiusOneText { get => GetLabelRadiusText(RadiusOne); }
        public string LabelRadiusTwoText { get => GetLabelRadiusText(RadiusTwo); }

        private string GetLabelRadiusText(int radius)
        {
            return Convert.ToString(radius) + " inches";
        }
    }
}