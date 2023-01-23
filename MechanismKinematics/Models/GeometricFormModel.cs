using System;

namespace MechanismKinematics
{
    public class GeometricFormModel
    {
        public int RadiusOne { get; set; }
        public int RadiusTwo { get; set; }
        public string LabelRadiusOneText { get => GetLabelRadiusText(RadiusOne); }
        public string LabelRadiusTwoText { get => GetLabelRadiusText(RadiusTwo); }

        private string GetLabelRadiusText(int radius)
        {
            return Convert.ToString(radius) + " inches";
        }
    }
}