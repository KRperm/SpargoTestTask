namespace WebApi.Dto.Responses
{
    public class ItemsNotExistsResponse
    {
        public IEnumerable<string> FieldsReferringToMissingItems { get; set; }
    }
}
