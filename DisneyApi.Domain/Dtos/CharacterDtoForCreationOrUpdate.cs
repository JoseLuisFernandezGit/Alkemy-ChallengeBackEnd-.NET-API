namespace DisneyApi.Domain.Dtos
{
    public class CharacterDtoForCreationOrUpdate
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string History { get; set; }
    }
}
