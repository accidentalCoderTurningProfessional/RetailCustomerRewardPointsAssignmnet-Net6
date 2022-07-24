namespace BusinessService.ServiceResponse
{
    public abstract class ServiceResponseOf : ServiceResponseWrapper
    {
        /// <summary>
        ///     Gets or sets the Message returned
        /// </summary>
        public virtual object Content { get; set; }
    }

    public class ServiceResponseOf<T> : ServiceResponseOf
    {
        public ServiceResponseOf() { }
        public ServiceResponseOf(ServiceResponseOf source)
        {
            if (source == null) { return; }
            Status = source.Status;
            ErrorId = source.ErrorId;
            ErrorPath = source.ErrorPath;
            Errors = source.Errors;
            ErrorSummary = source.ErrorSummary;
            RuleId = source.RuleId;
        }

        public ServiceResponseOf(T content) { base.Content = content; }

        #region Properties

        /// <summary>
        ///     Gets or sets the Message returned
        /// </summary>
        public new virtual T Content
        {
            get
            {
                //T could be a value type, and if base.Content was never 'set' then the default object value will
                //cause an error when trying to cast it as the value type, so always return default(T) instead
                if (base.Content == null) { return default(T); }
                return (T)base.Content;
            }
            set { base.Content = value; }
        }

        #endregion

    }
}
