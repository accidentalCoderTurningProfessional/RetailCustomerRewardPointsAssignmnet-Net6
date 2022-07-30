namespace RetailCustomerBonusCalculator.BusinessService.ServiceResponse
{
    public class ServiceResponseWrapper
    {
        public virtual bool Successful() { return Status != null && Status.Equals(ResponseStatuses.Success, StringComparison.InvariantCultureIgnoreCase); }
        public virtual bool Failed() { return !Successful(); }
        public virtual bool Warning() { return Status != null && (Status.Equals(ResponseStatuses.Warning, StringComparison.InvariantCultureIgnoreCase) || Status.Equals(ResponseStatuses.ValidationWarning, StringComparison.InvariantCultureIgnoreCase)); }

        public static class ResponseStatuses
        {
            public const string Success = "Success";
            public const string Error = "Error";
            public const string Warning = "Warning";
            public const string ValidationWarning = "ValidationWarning";
            public const string ValidationException = "ValidationException";
        }

        #region Properties

        private string _status = ResponseStatuses.Success;

        /// <summary>
        ///     Gets or sets the Status value of Success, Warning or Error.
        /// </summary>
      
        public virtual string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        ///     Gets or sets the ErrorSummary.
        /// </summary>
        public virtual int? ErrorId { get; set; }

        /// <summary>
        ///     Gets or sets the RuleID.
        /// </summary>
        public virtual int? RuleId { get; set; }

        /// <summary>
        ///     Gets or sets the path to the source of the error.
        /// </summary>
        public virtual string ErrorPath { get; set; }

        /// <summary>
        ///     Gets or sets the ErrorSummary.
        /// </summary>
        public virtual string ErrorSummary { get; set; }
        public virtual IList<string> Errors { get; set; }

        #endregion
    }
}
