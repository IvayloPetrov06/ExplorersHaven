using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.Models
{
    public class OfferViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public Decimal? MinPrice { get; set; }

        public Decimal? MaxPrice { get; set; }

        public List<Offer> Offers { get; set; }
        /*
         * <tbody>
        @if (Model.Offers.Count > 0)
        {
            @foreach (var offer in  Model.Offers)
            { 
            <tr>
                    <td>@offer.Id</td>
                    <td>@offer.Name</td>
                    <td>@offer.Price</td>
            </tr>
            }
        }
    </tbody>
         * */
    }
}
