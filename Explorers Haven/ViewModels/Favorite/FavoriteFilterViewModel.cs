using Explorers_Haven.ViewModels.Favorite;

namespace Explorers_Haven.ViewModels.Favorite
{
    public class FavoriteFilterViewModel
    {
        public string Search { get; set; }
        public List<FavoriteViewModel> Favorites { get; set; }
    }
}
