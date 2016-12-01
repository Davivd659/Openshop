
namespace YG.SC.Model
{
    public class UserAddressEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Gender { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public int CountyId { get; set; }
        public string CountyName { get; set; }

        public string AddressDetial { get; set; }
        public string InsBy { get; set; }
        public System.DateTime InsDt { get; set; }
        public string Recsts { get; set; }

        public string UserFullAddress
        {
            get { return string.Format("{0}{1}{2} {3}", this.ProvinceName, CityName, CountyName, AddressDetial); }
        }
    }
}
