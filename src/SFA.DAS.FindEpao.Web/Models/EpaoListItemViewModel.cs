namespace SFA.DAS.FindEpao.Web.Models
{
    public class EpaoListItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Locations { get; set; } //needs some logic
        public string Address { get; set; }//town/city, postcode
    }
}
