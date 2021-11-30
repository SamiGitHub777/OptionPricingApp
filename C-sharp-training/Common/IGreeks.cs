using System;

namespace C_sharp_training.Common
{
    public interface IGreeks
    {
        double delta();
        double gamma();
        double theta();
        double vega();
        double rho();
    }
}
