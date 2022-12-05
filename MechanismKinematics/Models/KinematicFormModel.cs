using System;

namespace MechanismKinematics
{
    public class KinematicFormModel
    {
        public string OmegaFullString { get; set; }

        public double GetOmega()
        {
            const double defaultSpeed = 0.5;
            try
            {
                const int afterNumberSymbolsCount = 6;
                return OmegaFullString != null ?
                    Convert.ToDouble(OmegaFullString.Remove
                    (OmegaFullString.Length - afterNumberSymbolsCount, afterNumberSymbolsCount)
                    .Replace(",", ".")) : defaultSpeed;
            }
            catch (Exception) 
            {
                return defaultSpeed;
            }
        }
    }
}