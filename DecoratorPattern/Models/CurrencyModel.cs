namespace DecoratorPattern.Models
{
    public class CurrencyModel : IResponseModel
    {
        public List<CurrencyDetail> Data { get; set; }
        public bool Success { get; set; }
    }

    public class CurrencyDetail
    {
        public string AlphabeticalCode { get; set; }
        public string NumericalCode { get; set; }
        public string Name { get; set; }
    }
}
