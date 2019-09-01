using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MyFinance.Data.DTOs.Results
{
    [DataContract]
    public enum BusinessResultState
    {
        [EnumMember]
        Success = 1,

        [EnumMember]
        Error = 2
    }

    [DataContract]
    public class Result
    {
        public Result()
        { }

        public Result(BusinessResultState state, string message = null)
        {
            State = state;
            Message = message;
        }

        [DataMember(Order = 0)]
        public BusinessResultState State { get; set; }

        [DataMember(Order = 1)]
        public string Message { get; set; }

        public bool IsError
        {
            get { return State == BusinessResultState.Error; }
        }

        public bool IsSuccess
        {
            get { return State == BusinessResultState.Success; }
        }
    }

    [DataContract(Name = "BusinessErrorOf{0}")]
    public class Result<Type> : Result
    {
        public Result()
        { }

        public Result(BusinessResultState state, Type returnValue, string message = null)
            : base(state, message)
        {
            ReturnValue = returnValue;
        }

        [DataMember(Order = 2)]
        public Type ReturnValue { get; set; }

        public static Result<Type> ErrorState(string message, Type returnValue = default(Type))
        {
            return new Result<Type>
            {
                ReturnValue = returnValue,
                State = BusinessResultState.Error,
                Message = message
            };
        }

        public static Result<Type> SuccessState(Type returnValue = default(Type))
        {
            return new Result<Type>
            {
                ReturnValue = returnValue,
                State = BusinessResultState.Success,
            };
        }
    }
}
