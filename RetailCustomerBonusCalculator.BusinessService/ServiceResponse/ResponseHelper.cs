using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailCustomerBonusCalculator.BusinessService.ServiceResponse
{
    public static class ResponseHelpers
    {
        public static async Task<ServiceResponseOf<TContent>> GetServiceResponseAsync<TContent>(Func<Task<TContent>> responseResultCallback, string failureMessage)
        {
            return await GetServiceResponseAsync(responseResultCallback, r => r, failureMessage);
        }

        public static async Task<ServiceResponseOf<TContent>> GetServiceResponseAsync<TAsync, TContent>(Func<Task<TAsync>> responseResultCallback, Func<TAsync, TContent> contentCallback, string failureMessage)
        {
            try
            {
                 return new ServiceResponseOf<TContent>
                {
                    Status = ServiceResponseWrapper.ResponseStatuses.Success,
                    Content = contentCallback(await responseResultCallback.Invoke()),
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseOf<TContent>
                {
                    Status = ServiceResponseWrapper.ResponseStatuses.Error,
                    ErrorSummary = ex.Message,
                    Errors = new List<string>(new[] {ex.ToString() })
                };
            }
        }

        //public static async Task<ServiceResponseOf<TContent>> GetServiceResponseAsync<TContent>(Func<Task<TContent>> responseResultCallback, Func<IEnumerable<FieldCharacteristic>> fieldCharacteristicsFunc, string failureMessage)
        //{
        //    return await GetServiceResponseAsync(responseResultCallback, r => r, fieldCharacteristicsFunc, failureMessage);
        //}

        //private static readonly Regex UserCharRegx = new Regex(@"[ \f\n\r\t\v\.\@\$\%\^\&\*\(\)\\\/\[\]\{\}\!\~]");
        //private static readonly Regex FieldCharRegx = new Regex(@"\s+");

        //private static List<FieldCharacteristic> FilterFieldChars(this IEnumerable<FieldCharacteristic> fieldChars)
        //{
        //    return (fieldChars ?? new List<FieldCharacteristic>())
        //        .Where(fc => fc.UserRequired || fc.UserHidden || fc.Name != fc.Caption)
        //        .Select(fc => new FieldCharacteristic
        //        {
        //            UserRequired = fc.UserRequired,
        //            UserHidden = fc.UserHidden,
        //            Caption = fc.Caption,
        //            Name = UserCharRegx.Replace(fc.Name ?? "", "").ToLowerInvariant().Replace("#", "num")
        //        })
        //        .ToNullableList();
        //}

        //public static async Task<ServiceResponseOf<TContent>> GetServiceResponseWithFieldCharacteristicsAsync<TContent>(Func<Task<TContent>> responseResultCallbackAsync, Func<Task<IEnumerable<FieldCharacteristic>>> fieldCharacteristicsFuncAsync, string failureMessage)
        //{
        //    try
        //    {
        //        if (fieldCharacteristicsFuncAsync == null) { return new ServiceResponseOf<TContent> { Status = ServiceResponseWrapper.ResponseStatuses.Success, Content = await responseResultCallbackAsync.Invoke() }; }

        //        return new ServiceResponseWithFieldCharacteristics<TContent>
        //        {
        //            Status = ServiceResponseWrapper.ResponseStatuses.Success,
        //            Content = await responseResultCallbackAsync.Invoke(),
        //            FieldCharacteristics = (await fieldCharacteristicsFuncAsync.Invoke()).FilterFieldChars()
        //        };
        //    }
        //    catch (SecurityException) { throw; }
        //    catch (FeDomainException ex) { return ex.ToBaseResponseMessage<TContent>(failureMessage); }
        //    catch (Exception ex) { return ex.ToBaseResponseMessage<TContent>(failureMessage); }
        //}
        //public static async Task<ServiceResponseOf<TContent>> GetServiceResponseWithFieldCharacteristicsAsync<TContent>(Func<Task<TContent>> responseResultCallbackAsync, Func<TContent, IEnumerable<FieldCharacteristic>> fieldCharacteristicsFuncAsync, string failureMessage)
        //{
        //    try
        //    {
        //        if (fieldCharacteristicsFuncAsync == null) { return new ServiceResponseOf<TContent> { Status = ServiceResponseWrapper.ResponseStatuses.Success, Content = await responseResultCallbackAsync.Invoke() }; }

        //        var ret = new ServiceResponseWithFieldCharacteristics<TContent>
        //        {
        //            Status = ServiceResponseWrapper.ResponseStatuses.Success,
        //            Content = await responseResultCallbackAsync.Invoke(),
        //        };
        //        ret.FieldCharacteristics = fieldCharacteristicsFuncAsync.Invoke(ret.Content).FilterFieldChars();
        //        return ret;
        //    }
        //    catch (SecurityException) { throw; }
        //    catch (FeDomainException ex) { return ex.ToBaseResponseMessage<TContent>(failureMessage); }
        //    catch (Exception ex) { return ex.ToBaseResponseMessage<TContent>(failureMessage); }
        //}

        ///// <summary>
        ///// ToResponse should only be used in simple cases where no error can occur in the creation of the object - use ResponseHelpers.GetServiceResponse instead
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="content"></param>
        ///// <param name="status"></param>
        ///// <returns></returns>
        //public static ServiceResponseOf<T> ToResponse<T>(this T content, string status = ServiceResponseWrapper.ResponseStatuses.Success) { return new ServiceResponseOf<T>(content) { Status = status }; }

        //public static ServiceResponseWrapper CopyToResponse(this Exception ex, ServiceResponseWrapper response, string failureMessage)
        //{
        //    var vEx = ex as FeValidationException;
        //    if (vEx != null) { return vEx.CopyToResponse(response, failureMessage); }

        //    var dEx = ex as FeDomainException;
        //    response.ErrorSummary = failureMessage ?? "Unable to perform requested operation";
        //    response.Errors = new List<string>();
        //    if (dEx != null)
        //    {
        //        response.RuleId = dEx.RuleId.GetValueOrDefault();
        //        var isWarning = dEx.IsWarningRuleViolation();
        //        response.ErrorId = isWarning ? response.RuleId : dEx.ErrorId; //this should not be overloaded 
        //        response.ErrorPath = dEx.ErrorPath;
        //        response.Status = isWarning ? ServiceResponseWrapper.ResponseStatuses.Warning : ServiceResponseWrapper.ResponseStatuses.Error;
        //    }
        //    else
        //    {
        //        response.Status = ServiceResponseWrapper.ResponseStatuses.Error;
        //    }

        //    ex = ex.FilterException();

        //    while (ex != null)
        //    {
        //        response.Errors.Add(ex.Message.NormalizeNewlines());
        //        ex = ex.InnerException;
        //    }

        //    return response;
        //}

        //public static ServiceResponseWrapper CopyToResponse(this FeValidationException ex, ServiceResponseWrapper response, string failureMessage)
        //{
        //    var isWarning = ex.IsWarningRuleViolation();
        //    response.Errors = new List<string>(new[] { ex.FilterException().Message.NormalizeNewlines() });
        //    response.ErrorId = isWarning ? ex.RuleId.GetValueOrDefault() : ex.ErrorId;
        //    response.ErrorPath = ex.ErrorPath;
        //    response.Status = isWarning ? ServiceResponseWrapper.ResponseStatuses.ValidationWarning : ServiceResponseWrapper.ResponseStatuses.ValidationException;
        //    response.ErrorSummary = failureMessage ?? "Unable to perform requested operation";
        //    return response;
        //}

        //public static ServiceResponseOf<T> ToBaseResponseMessage<T>(this Exception ex, string failureMessage) { return ex.CopyToResponse(new ServiceResponseOf<T>(), failureMessage) as ServiceResponseOf<T>; }

        //public static ServiceResponseOf<T> ToBaseResponseMessage<T>(this FeValidationException ex, string failureMessage) { return ex.CopyToResponse(new ServiceResponseOf<T>(), failureMessage) as ServiceResponseOf<T>; }

        //public static ServiceResponseOf<T> GetServiceResponse<T>(Func<T> responseResultCallback, Func<IEnumerable<FieldCharacteristic>> fieldCharFunc, string failureMessage)
        //{
        //    try
        //    {
        //        if (fieldCharFunc == null)
        //        {
        //            return new ServiceResponseOf<T>
        //            {
        //                Status = ServiceResponseWrapper.ResponseStatuses.Success,
        //                Content = responseResultCallback.Invoke()
        //            };
        //        }

        //        return new ServiceResponseWithFieldCharacteristics<T>
        //        {
        //            Status = ServiceResponseWrapper.ResponseStatuses.Success,
        //            Content = responseResultCallback.Invoke(),
        //            FieldCharacteristics = fieldCharFunc().FilterFieldChars()
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex
        //            .ThrowIfTerminal()
        //            .ToBaseResponseMessage<T>(failureMessage);
        //    }
        //}

        //public static ServiceResponseOf<T> GetServiceResponse<T>(Func<T> responseResultCallback, string failureMessage) { return GetServiceResponse(responseResultCallback, null, failureMessage); }

        //public static ServiceResponseOf<bool> GetBoolResponseMessage<T>(Func<T> responseResultCallback, string failureMessage)
        //{
        //    var ret = GetServiceResponse(responseResultCallback, failureMessage);
        //    return new ServiceResponseOf<bool>
        //    {
        //        Content = ret.Successful(),
        //        ErrorId = ret.ErrorId,
        //        RuleId = ret.RuleId,
        //        ErrorPath = ret.ErrorPath,
        //        Errors = ret.Errors,
        //        ErrorSummary = ret.ErrorSummary,
        //        Status = ret.Status
        //    };
        //}
    }
}
