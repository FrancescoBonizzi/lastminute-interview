﻿using Castle.Windsor;
using System.Threading;

namespace SalesTaxes_ClientConsole
{
    /// <summary>
    /// A console application that renders shopping carts receipts
    /// </summary>
    public class Program
    {
        private static void Main()
        {
            var dependenciesInstaller = new DependenciesInstaller();
            var dependencyContainer = new WindsorContainer();
            dependencyContainer.Install(dependenciesInstaller);

            var interviewInput1 = dependencyContainer.Resolve<InterviewInput1>();
            interviewInput1.Run();

            // A delay to let the browser start
            Thread.Sleep(500);

            var interviewInput2 = dependencyContainer.Resolve<InterviewInput2>();
            interviewInput2.Run();

            // A delay to let the browser start
            Thread.Sleep(500);

            var interviewInput3 = dependencyContainer.Resolve<InterviewInput3>();
            interviewInput3.Run();
        }
    }
}
