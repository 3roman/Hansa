using System;

namespace HASA
{
    internal static class Common
    {
        public static double CalculatePipeWeight(int od, double pipewall)
        {
            return 0.02466 * (od - pipewall) * pipewall;
        }

        public static double CalculateWaterWeight(double od, double pipewall)
        {
            od /= 1000;
            pipewall /= 1000;

            return 0.785 * Math.Pow(od - 2 * pipewall, 2) * 1000;
        }

        public static double CalculateInsulationWeight(double od, double insulation)
        {
            od /= 1000;
            insulation /= 1000;

            return 0.785 * (Math.Pow(od + 2 * insulation, 2) - Math.Pow(od, 2)) * 200;
        }
    }
}
