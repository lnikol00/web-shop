using System.ComponentModel.DataAnnotations;

namespace WebShopApp.DAL.Enums
{
    public enum Category
    {
        [Display(Name = "None", Description = "No category selected")]
        none,

        [Display(Name = "Beauty", Description = "Beauty products")]
        beauty,

        [Display(Name = "Fragrances", Description = "Fragrances and perfumes")]
        fragrances,

        [Display(Name = "Furniture", Description = "Furniture items")]
        furniture,

        [Display(Name = "Groceries", Description = "Groceries and essentials")]
        groceries,

        [Display(Name = "Home Decoration", Description = "Decorative items for the home")]
        home_decoration,

        [Display(Name = "Kitchen Accessories", Description = "Kitchen gadgets and tools")]
        kitchen_accessories,

        [Display(Name = "Laptops", Description = "Laptop computers and accessories")]
        laptops,

        [Display(Name = "Mens Shirts", Description = "Men's shirts and clothing")]
        mens_shirts,

        [Display(Name = "Mens Shoes", Description = "Men's shoes and footwear")]
        mens_shoes,

        [Display(Name = "Mens Watches", Description = "Men's watches and timepieces")]
        mens_watches,

        [Display(Name = "Mobile Accessories", Description = "Mobile phone accessories")]
        mobile_accessories,

        [Display(Name = "Motorcycle", Description = "Motorcycles and accessories")]
        motorcycle,

        [Display(Name = "Skin Care", Description = "Skincare products")]
        skin_care,

        [Display(Name = "Smartphones", Description = "Smartphones and mobile phones")]
        smartphones,

        [Display(Name = "Sports Accessories", Description = "Sports and fitness accessories")]
        sports_accessories,

        [Display(Name = "Sunglasses", Description = "Sunglasses and eyewear")]
        sunglasses,

        [Display(Name = "Tablets", Description = "Tablets and e-readers")]
        tablets,

        [Display(Name = "Tops", Description = "Tops and upper clothing")]
        tops,

        [Display(Name = "Vehicle", Description = "Vehicles and automotive")]
        vehicle,

        [Display(Name = "Womens Bags", Description = "Women's bags and accessories")]
        womens_bags,

        [Display(Name = "Womens Dresses", Description = "Women's dresses and apparel")]
        womens_dresses,

        [Display(Name = "Womens Jewellery", Description = "Women's jewelry and ornaments")]
        womens_jewellery,

        [Display(Name = "Womens Shoes", Description = "Women's shoes and footwear")]
        womens_shoes,

        [Display(Name = "Womens Watches", Description = "Women's watches and timepieces")]
        womens_watches
    }
}
