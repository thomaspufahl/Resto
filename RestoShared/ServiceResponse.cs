using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared
{
    public enum ServiceResponseStatus
    {
        Success,
        Fail
    }
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public ServiceResponseStatus Status { get; set; }
        public T Data { get; set; }
        public ServiceResponse() { }
        public ServiceResponse(ServiceResponseStatus status, T data)
        {
            Status = status;
            Data = data;
        }
        public ServiceResponse(ServiceResponseStatus status, string message)
        {
            Status = status;
            Data = default;
            Message = message;
        }
        public ServiceResponse(ServiceResponseStatus status, T data, string message)
        {
            Status = status;
            Data = data;
            Message = message;
        }

        public static ServiceResponse<T> Success(T data)
        {
            return new ServiceResponse<T>(ServiceResponseStatus.Success, data);
        }
        public static ServiceResponse<T> Success(T data, string message)
        {
            return new ServiceResponse<T>(ServiceResponseStatus.Success, data, message);
        }

        public static ServiceResponse<T> Fail(T data)
        {
            return new ServiceResponse<T>(ServiceResponseStatus.Fail, data);
        }
        public static ServiceResponse<T> Fail(string message)
        {
            return new ServiceResponse<T>(ServiceResponseStatus.Fail, message);
        }
    }
}
