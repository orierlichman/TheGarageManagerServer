namespace TheGarageManagerServer.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string? Email { get; set; }
        public string? UserPassword { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public int UserGarageID { get; set; }
        public int? UserStatusID { get; set; }


        public UserDTO() { }

        public UserDTO(Models.User modelUser)
        {
            this.UserID = modelUser.UserId;
            this.Email = modelUser.Email;
            this.UserPassword = modelUser.UserPassword;
            this.UserFirstName = modelUser.UserFirstName;
            this.UserLastName = modelUser.UserLastName;
            this.UserGarageID = modelUser.UserGarageId;
            this.UserStatusID = modelUser.UserStatusId;
        }

        public Models.User GetModels()
        {
            Models.User modelsUser = new Models.User()
            {
                UserId = this.UserID,
                Email = this.Email,
                UserPassword = this.UserPassword,
                UserFirstName = this.UserFirstName,
                UserLastName = this.UserLastName,
                UserGarageId = this.UserGarageID,
                UserStatusId = this.UserStatusID
            };
            return modelsUser;
        }

    }
}
