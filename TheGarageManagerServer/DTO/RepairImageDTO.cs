namespace TheGarageManagerServer.DTO
{
    public class RepairImageDTO
    {
        public int ImageID { get; set; }
        public int RepairID { get; set; }
        public string ImageURL { get; set; }  
        public DateTime UploadDate { get; set; } 
    }
}
