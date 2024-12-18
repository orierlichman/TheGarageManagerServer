namespace TheGarageManagerServer.DTO
{
    public class GaragePartsDTO
    {
        public int PartID { get; set; }
        public int? GarageID { get; set; }
        public string? PartName { get; set; }
        public int? PartNumber { get; set; }
        public int? Cost { get; set; }
        public string? ImageURL { get; set; }


        public GaragePartsDTO() { }

        public GaragePartsDTO(Models.GaragePart modelParts)
        {
            this.PartID = modelParts.PartId;
            this.GarageID = modelParts.GarageId;
            this.PartName = modelParts.PartName;
            this.PartNumber = modelParts.PartNumber;
            this.Cost = modelParts.Cost;
            this.ImageURL = modelParts.ImageUrl;
        }

        public Models.GaragePart GetGarageParts()
        {
            Models.GaragePart modelParts = new Models.GaragePart()
            {
                PartId = this.PartID,
                GarageId = this.GarageID,
                PartName = this.PartName,
                PartNumber = this.PartNumber,
                Cost = this.Cost,
                ImageUrl = this.ImageURL
            };
            return modelParts;
        }

    }
}
