using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Client;
using PureCloudPlatform.Client.V2.Extensions;
using PureCloudPlatform.Client.V2.Model;
using System.Diagnostics;
using System.Numerics;
using webEcontact.Models;
using webEcontact.Servicio.Interface;

namespace webEcontact.Servicio.Repositorio
{
    public class RContactLIst : IContactList
    {
        private readonly IConfiguration _config;

        public RContactLIst(IConfiguration configuration)
        {
            _config = configuration;
        }

        #region Obtener las contactlist
        public async Task<List<EC_Contactlist>> obtenerContactList()
        {
            List<EC_Contactlist> lContactlist = new List<EC_Contactlist>();

            obtenerToken();

            #region Obtener las contactlist

            lContactlist = listarContactlist();

            #endregion

            return lContactlist;
        }
        #endregion

        #region Obtener Token
        public bool obtenerToken()
        {
            var cliID = _config.GetValue<string>("GenesysConfiguration:ClientID");
            var cliPass = _config.GetValue<string>("GenesysConfiguration:ClientPass");

            PureCloudRegionHosts region = PureCloudRegionHosts.us_east_1; // Genesys Cloud region
            Configuration.Default.ApiClient.setBasePath(region);

            try
            {
                var accessTokenInfo = PureCloudPlatform.Client.V2.Client.Configuration.Default.ApiClient.PostToken(cliID, cliPass);
                var token_PC = accessTokenInfo.AccessToken;
                if (string.IsNullOrEmpty(token_PC))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ApiException aEx)
            {
                Console.WriteLine($"Error al generar el token| codigo:{aEx.ErrorCode}| mensaje:{aEx.Message}");
                throw;
            }

        }
        #endregion

        #region Obtener Listas de contacto
        public List<EC_Contactlist> listarContactlist()
        {
            List<EC_Contactlist> lContactlist = new List<EC_Contactlist>();
            bool flag = true;

            ContactListEntityListing contactlistResult = new ContactListEntityListing();

            try
            {
                var apiInstance = new OutboundApi();
                var includeSize = true;  // bool? | Include size (optional)  (default to false)
                var pageSize = 100;  // int? | Page size. The max that will be returned is 100. (optional)  (default to 25)
                var pageNumber = 1;  // int? | Page number (optional)  (default to 1)
                var id = new List<string>(); // List<string> | id (optional) 
                var divisionId = new List<string>(); // List<string> | Division ID(s) (optional) 
                var sortBy = "name";  // string | Sort by (optional) 
                var sortOrder = "ascending";  // string | Sort order (optional)  (default to a)

                ////var includeImportStatus = true;  // bool? | Include import status (optional)  (default to false)
                ////var allowEmptyResult = true;  // bool? | Whether to return an empty page when there are no results for that page (optional)  (default to false)
                ////var filterType = "filterType_example";  // string | Filter type (optional)  (default to Prefix)
                ////var name = "name_example";  // string | Name (optional) 


                while (flag)
                {
                    ContactListEntityListing result = apiInstance.GetOutboundContactlists(null, includeSize, pageSize, pageNumber, 
                                                                                          null, null, null, null, 
                                                                                          null, sortBy, sortOrder);

                    if (result.Entities != null && result.Entities.Count > 0)
                    {
                        foreach (var item in result.Entities)
                        {
                            EC_Contactlist contactlist = new EC_Contactlist
                            {
                                size = item.Size ?? 0,
                                name = item.Name.ToUpper(),
                                id = item.Id,
                                divisionName = item.Division?.Name,
                                fechaCreacion = item.DateCreated.HasValue ? item.DateCreated.Value.ToString("dd-MM-yyyy") : null
                            };
                            lContactlist.Add(contactlist);
                        }
                        pageNumber++;
                    }

                    if (result.PageCount == result.PageNumber)
                    {
                        flag = false;    
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            catch (ApiException aEx)
            {
                Console.WriteLine($"Error al obtener las contactlist en el api de genesys cloud| codigo:{aEx.ErrorCode}| mensaje:{aEx.Message}");
                throw;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al obtener las contact list| mensaje:{ex.Message}");
                throw;
            }
            return lContactlist;
        }
        #endregion

        #region Obtener divissiones
        public List<EC_Division> ObtenerDivisiones()
        {

            List<EC_Division> lDivisiones = new List<EC_Division>();
            bool flag = true;

            AuthzDivisionEntityListing result = new AuthzDivisionEntityListing();

            try
            {
                

                var apiInstance = new ObjectsApi();
                var pageSize = 100;  // int? | The total page size requested (optional)  (default to 25)
                var pageNumber = 1;  // int? | The page number requested (optional)  (default to 1)
                var sortBy = "name";  // string | variable name requested to sort by (optional) 
                var expand = new List<string>(); // List<string> | variable name requested by expand list (optional) 
                var nextPage = "nextPage_example";  // string | next page token (optional) 
                var previousPage = "previousPage_example";  // string | Previous page token (optional) 
                var objectCount = true;  // bool? | Include the count of objects contained in the division (optional)  (default to false)
                var id = new List<string>(); // List<string> | Optionally request specific divisions by their IDs (optional) 
                var name = "name_example";  // string | Search term to filter by division name (optional) 



                while (flag)
                {
                    // Retrieve a list of all divisions defined for the organization
                    result = apiInstance.GetAuthorizationDivisions(pageSize, pageNumber, sortBy, null, 
                                                                                                null, null, null, null, null);


                    if (result.Entities != null && result.Entities.Count > 0)
                    {
                        foreach (var item in result.Entities)
                        {
                            EC_Division division = new EC_Division
                            {
                                name = item.Name.ToUpper(),
                                id = item.Id,
                            };
                            lDivisiones.Add(division);
                        }
                        pageNumber++;
                    }

                    if (result.PageCount == result.PageNumber)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            catch (ApiException aEx)
            {
                Console.WriteLine($"Error al obtener las contactlist en el api de genesys cloud| codigo:{aEx.ErrorCode}| mensaje:{aEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las contact list| mensaje:{ex.Message}");
                throw;
            }
            return lDivisiones;

        }
        #endregion



    }
}
