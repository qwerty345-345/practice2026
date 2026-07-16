using System;
using System.Threading.Tasks;

namespace task14
{
    public static class DefiniteIntegral
    {
        public static double Solve(double a, double b, Func<double, double> f, double step, int threads)
        {
            if (threads <= 0)
                throw new ArgumentException("Количество потоков должно быть больше нуля.", nameof(threads));

            double totalWidth = b - a;
            long totalSteps = (long)Math.Ceiling(totalWidth / step);
            double actualStep = totalWidth / totalSteps;

            double globalSum = 0.0;
            object lockObject = new object();

            Parallel.For(0, threads, t =>
            {
                long stepsPerThread = totalSteps / threads;
                long startStep = t * stepsPerThread;
                long endStep = (t == threads - 1) ? totalSteps : startStep + stepsPerThread;

                double localSum = 0.0;
                for (long i = startStep; i < endStep; i++)
                {
                    double x = a + (i + 0.5) * actualStep;
                    localSum += f(x);
                }

                lock (lockObject)
                {
                    globalSum += localSum;
                }
            });

            double result = globalSum * actualStep;
            Console.WriteLine(result); // Тот самый вывод значения в консоль блокнота
            return result;
        }
    }
}
