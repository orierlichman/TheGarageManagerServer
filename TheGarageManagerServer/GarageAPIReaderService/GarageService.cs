using TheGarageManagerServer.Models;

namespace TheGarageManagerServer.GarageAPIReaderService
{
    public class GarageService
    {
        
        

        public async Task<Garage?> ImportGarageFromApiAsync(string rashamHavarot)
        {
            ApiService apiService = new ApiService();
            string apiUrl = "https://data.gov.il/api/3/action/datastore_search";
            string resourceId = $"bb68386a-a331-4bbc-b668-bba2766d517d&q={rashamHavarot}"; // ה-resource_id שלך

            // קריאת הנתונים מה-API
            var records = await apiService.GetApiData(apiUrl, resourceId);

            Garage? g = null;

            // הוספת הנתונים לבסיס הנתונים
            if (records.Count > 0)
            {
                var record = records[0];
                g = new Garage()
                {
                    RashamHavarot = record.rasham_havarot.ToString(),
                    MosahNumber = record.mispar_mosah,
                    GarageName = record.shem_mosah,
                    TypeCode = record.cod_sug_mosah,
                    GarageType = record.sug_mosah,
                    GarageAddress = record.ktovet,
                    City = record.yishuv,
                    Phone = record.telephone,
                    ZipCode = record.mikud.ToString(),
                    SpecializationCode = record.cod_miktzoa,
                    Specialization = record.miktzoa,
                    ManagerSpecialization = record.menahel_miktzoa,
                    License = record.rasham_havarot,
                };

            }
            
            return g;
        }
    }
}
