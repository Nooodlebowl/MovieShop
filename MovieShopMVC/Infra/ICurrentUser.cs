namespace MovieShopMVC.Infra
{
    public interface ICurrentUser
    {
        public int UserId { get; }
        public bool IsAdmin { get; }
        public bool IsAuthenticated { get; }
        public string Email { get; }
        public string ProfilePictureUrl { get;  }
        public string FullName { get; }

    }
}
