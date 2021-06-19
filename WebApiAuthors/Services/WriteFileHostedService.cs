using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiAuthors.Services
{
    public class WriteFileHostedService : IHostedService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _fileName = $"{DateTime.Now.Day}_hosted_service.txt";
        private Timer timer;

        public WriteFileHostedService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Write("Process start.");
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            Write("Process end.");
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Write($"Proceso en ejecución: {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }

        private void Write(string message)
        {
            var path = $@"{_env.ContentRootPath}\wwwroot";
            using var writer = new StreamWriter(Path.Combine(path, _fileName), append: true, encoding: Encoding.UTF8);
            writer.WriteLine(message);
            writer.Close();
        }
    }
}
