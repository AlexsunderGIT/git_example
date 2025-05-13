namespace ConsoleProject.NET.DTO.User
{
    public class AuthentificationResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public AuthentificationResponseDto (int id, string name, string token)
        {
            this.Id = id;
            this.Name = name;
            this.Token = token;
        }
    }
}