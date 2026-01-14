namespace VendingSystemWeb.Models
{
    // Этот класс точно описывает, что мы получаем от API после успешного входа
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}
