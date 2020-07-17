using System;
using DependencyInjection.Sample.Services.GuidGenerator;

namespace DependencyInjection.Sample.Services.SomeService
{
    public class SomeService : ISomeService
    {
        private readonly IGuidGenerator _guidGenerator;

        private bool _disposed;

        public bool Disposed => _disposed;

        public SomeService()
        {
            _guidGenerator = new RandomGuidGenerator();
        }

        public SomeService(IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }

        public void PrintGuid()
        {
            Console.WriteLine(_guidGenerator.Guid);
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}