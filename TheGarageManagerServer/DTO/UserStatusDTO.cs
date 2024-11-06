namespace TheGarageManagerServer.DTO
{
    public class UserStatusDTO
    {
        public int StatusID { get; set; }
        public string? StatusName { get; set; }  
        public UserStatusDTO(Models.UserStatus status)
        {
            StatusID = status.StatusId;
            StatusName = status.StatusName;
        }
    }
}
