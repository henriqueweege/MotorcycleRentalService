namespace MotorcycleRentalService.Api.Validations
{
    public class DocumentInputValidator
    {
        public DocumentInputValidator(string base64String)
        {
            Base64String = base64String;
        }
        public string Base64String { get; set; }

        public bool IsValid()
        {
            var toEvaluate = Base64String.Substring(0, 10);
            return toEvaluate.ToUpper().StartsWith("IVBOR") || toEvaluate.StartsWith("Qk2");
        }
    }
}
