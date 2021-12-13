namespace DisneyApi.Domain.Dtos
{
    public class MovieDtoForCreationOrUpdate
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string CreationDate { get; set; }
        public int Qualification { get; set; }
        public int GenreId { get; set; }
    }
}
