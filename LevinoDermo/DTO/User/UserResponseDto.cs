namespace ConsoleProject.NET.DTO.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserResponseDto(int id, string name)
        {
            this.Id = id;
            Name = name;
        }
    }
}