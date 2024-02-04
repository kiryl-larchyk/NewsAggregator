namespace NewsAggregator.BusinessLogic.Infastructure
{
    public class NewsServiceException : Exception
    {
        public NewsServiceException(string message, Exception innerException) 
            : base(message, innerException)
        { 
        }
    }
}
