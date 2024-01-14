using AlpayMakina.Dtos.SubCategoryDtos;

namespace AlpayMakina.Dtos.CategoryDtos
{
    public class ResultCategoryWithSubCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public List<SubCategory> SubCategoryList { get; set; }
    }
}
