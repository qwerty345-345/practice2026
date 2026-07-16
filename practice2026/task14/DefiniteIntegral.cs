using System;
using System.Threading;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double sum = 0;

        long totalSteps = (long)Math.Round((b - a) / step);

        long part = totalSteps / threadsnumber;

        Barrier barrier = new Barrier(threadsnumber + 1);

        for (int i = 0; i < threadsnumber; i++)
        {
            int index = i;

            new Thread(() =>
            {
                long start = index * part;

                long end = index == threadsnumber - 1
                    ? totalSteps
                    : start + part;

                double local = 0;

                for (long j = start; j < end; j++)
                {
                    double x1 = a + j * step;
                    double x2 = x1 + step;

                    local += (function(x1) + function(x2)) * step / 2;
                }

                double oldValue;
                double newValue;

                do
                {
                    oldValue = sum;
                    newValue = oldValue + local;
                }
                while (Interlocked.CompareExchange(ref sum, newValue, oldValue) != oldValue);

                barrier.SignalAndWait();
            }).Start();
        }

        barrier.SignalAndWait();

        Console.WriteLine(sum);

        return sum;
    }
}