namespace Shoppinglist_back.Dtos.RelatedUsersDtos
{
    public class ReadRelatedUsersByUserIdDto
    {
        public string UserId {  get; set; }
        public string UserName { get; set; }
        public string? Nickname { get; set; }
    }
}
