using System;

namespace MechanismKinematicsWinFormsMVP
{
    public class KinematicFormModel
    {
        public string OmegaFullString { get; set; }

        public double GetOmega()
        {
            double defaultSpeed = 0.5;
            try
            {
                int afterNumberSymbolsCount = 6;
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
