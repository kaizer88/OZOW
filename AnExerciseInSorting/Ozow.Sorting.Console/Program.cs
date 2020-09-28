
namespace Ozow.Sorting.Console
{
    using System;
    using Autofac;
    using Ozow.Sorting.Domain.DomainServices;
    using Ozow.Sorting.Domain.DomainServices.SortAlgo;

    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                _printUsage();
                return;
            }
            
            // Build the DI container
            var container = _buildContainer();

            // Get Main soman service and call with input
            var sortSvc = container.Resolve<ISortDomainService>();            
            var output = sortSvc.Sort(args[0]);

            // Print the output to console
            Console.WriteLine(output);            
        }

        private static IContainer _buildContainer()
        {
            var builder = new ContainerBuilder();

            // Scan assemblies
            builder.RegisterAssemblyTypes(new[]{
                typeof(ISortDomainService).Assembly,
                typeof(Program).Assembly,
            })
            .AsImplementedInterfaces();

            // User Quicksort as the default sort algo
            builder.RegisterType<QuickSort>().As<ISortAlgo>();

            return builder.Build();
        }

        private static void _printUsage()
        {
            //Console.WriteLine("Contrary to popular belief, the pink unicorn flies east.");
            // Arrange
            var input = "Contrary to popular belief, the pink unicorn flies east.";

            // Act
            var sut = new QuickSort();
            var output = sut.Sort(input);
        }
    }
}
