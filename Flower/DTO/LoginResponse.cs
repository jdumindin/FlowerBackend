namespace Flower.DTO
{
    public class LogInResponse
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public required string Token { get; set; }
    }
}
