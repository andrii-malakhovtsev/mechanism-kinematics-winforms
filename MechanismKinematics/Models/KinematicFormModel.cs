using System;

namespace MechanismKinematics
{
    public class KinematicFormModel
    {
        public string OmegaFullString { private get; set; }

        public double Omega
        {
            get
            {
                const double defaultSpeed = 0.5;
                try
                {
                    const int afterNumberSymbolsCount = 6;
                    return OmegaFullString != null ? Convert.ToDouble(OmegaFullString.Remove
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
}