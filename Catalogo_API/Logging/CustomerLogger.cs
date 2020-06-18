using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_API.Logging
{
    public class CustomerLogger : ILogger
    {

        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
            EscreverTextoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivoLog = @"C:\dev\log\log.txt";
            using (StreamWriter stramWrite = new StreamWriter(caminhoArquivoLog, true))
            {
                try
                {
                    stramWrite.WriteLine(mensagem);
                    stramWrite.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
