using System;

namespace StepApp.CommonExtensions.Logger
{
   
    public interface ILoggerService<T> 
    {
        void Info(string message);

        void Debug(string message);

        void Error(string message);
        void Error(Exception message);

        void Warn(string message);


    }
}
