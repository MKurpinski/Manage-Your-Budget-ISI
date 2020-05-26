namespace ManageYourBudget.Dtos.Profile
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PictureSrc { get; set; }
        public bool HasLocalAccount { get; set; }
        public bool HasAnyWallet { get; set; }
    }
}
