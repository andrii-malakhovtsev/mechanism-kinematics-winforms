using System;

namespace MechanismKinematics
{
    public class GeometricFormModel
    {
        private int _radiusOne;
        private int _radiusTwo;

        public int RadiusOne 
        { 
            get => _radiusOne;
            set => MechanismController.SetRadius(ref _radiusOne, value);
        }

        public int RadiusTwo 
        { 
            get => _radiusTwo;
            set => MechanismController.SetRadius(ref _radiusTwo, value);
        }

        public string LabelRadiusOneText => GetLabelRadiusText(RadiusOne);

        public string LabelRadiusTwoText => GetLabelRadiusText(RadiusTwo); 
        //mb can get radius into separate class having fields like
        // string Radius.LabelText

        private string GetLabelRadiusText(int radius) => Convert.ToString(radius) + " inches";
    }
}