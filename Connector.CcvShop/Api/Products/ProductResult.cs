using Connector.CcvShop.Api.Base;
using System;

namespace Connector.CcvShop.Api.Products
{
    public class ProductResult : ResultBase
    {
        /// <summary>
        /// Unique id of the resource, minimum 1
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Create date of product in UTC
        /// </summary>
        public DateTime createdate { get; set; }

        /// <summary>
        /// Last modify date of product in UTC
        /// </summary>
        public DateTime modifydate { get; set; }

        /// <summary>
        /// Product number, maxlength: 50
        /// </summary>
        public string productnumber { get; set; }

        /// <summary>
        /// EAN(European Article Number), maxlength: 50
        /// </summary>
        public string eannumber { get; set; }

        /// <summary>
        /// Manufacturer Product Number, maxlength: 50
        /// </summary>
        public string mpnnumber { get; set; }

        /// <summary>
        /// Product name, maxlength: 200
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Product short description, maxlength: 255
        /// </summary>
        public string shortdescription { get; set; }

        /// <summary>
        /// Product description, maxlength: 65536
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// VAT rate of product in percentage, 0-100
        /// Required
        /// </summary>
        public double? vatrate { get; set; }

        /// <summary>
        /// Original price of the product, before discounts, minimum: 0
        /// Required
        /// </summary>
        public double? price { get; set; }

        /// <summary>
        /// Discount on the product.Price - Discount = Sell price
        /// Required
        /// </summary>
        public double? discount { get; set; }

        /// <summary>
        /// Purchase price ex.VAT off this product. Minimum: 0
        /// Required
        /// </summary>
        public double? purchaseprice { get; set; }

        /// <summary>
        /// Custom creditpoints for this product
        /// </summary>
        public int? credit_points_custom { get; set; }

        /// <summary>
        /// Calculated creditpoints for this product
        /// </summary>
        public int? credit_points_calculated { get; set; }

        /// <summary>
        /// The unit in which this product is sold (ie 'per piece')
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// This product has stock
        /// Required
        /// </summary>
        public bool? stockenabled { get; set; }

        /// <summary>
        /// If the stock amount is linked to the product of the attribute combination
        /// </summary>
        /// <value>Product</value>
        /// <value>Attribute</value>
        public string stocktype { get; set; }

        /// <summary>
        /// The quantity in stock for this product
        /// </summary>
        public int? stock { get; set; }

        /// <summary>
        /// The location of the product's stock, maxlength: 255
        /// </summary>
        public string stocklocation { get; set; }

        /// <summary>
        /// Weight of the product in kilograms, minimum: 0
        /// Required
        /// </summary>
        public double? weight { get; set; }

        /// <summary>
        /// The main categorie of this product.
        /// </summary>
        public string maincategory { get; set; }

        /// <summary>
        /// The sub categorie of this product.
        /// </summary>
        public string subcategory { get; set; }

        //TODO: Add Brand
        //TODO: Add condition
        
        /// <summary>
        /// Link to mainphoto
        /// </summary>
        public Uri productmainphoto { get; set; }

        /// <summary>
        /// Metatag Description
        /// </summary>
        public string meta_description { get; set; }

        /// <summary>
        /// Metatag Keywords
        /// </summary>
        public string meta_keywords { get; set; }

        /// <summary>
        /// Page title
        /// </summary>
        public string page_title { get; set; }

        /// <summary>
        /// Metatag robots: No-Index
        /// Required
        /// </summary>
        public bool? no_index { get; set; }

        /// <summary>
        /// Metatag robots: No-Follow
        /// Required
        /// </summary>
        public bool? no_follow { get; set; }

        /// <summary>
        /// SEO Alias of this resource
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// Deeplink to this resource
        /// </summary>
        public string deeplink { get; set; }

        /// <summary>
        /// Specification link
        /// </summary>
        public Uri specs { get; set; }

        /// <summary>
        /// Minimal order amount, min: 1
        /// Required
        /// </summary>
        public int? minimal_order_amount { get; set; }

        /// <summary>
        /// Expected delivery time, number of days, weeks, months, quarters, years, value between: 1-31
        /// Required
        /// </summary>
        public int? stock_delivery_number { get; set; }

        /// <summary>
        /// Expected delivery type
        /// </summary>
        /// <value>days</value>
        /// <value>weeks</value>
        /// <value>months</value>
        /// <value>quarters</value>
        /// <value>years</value>
        /// <value>outofstock</value>
        /// <value>temporarilysoldout</value>
        /// <value>ordered</value>
        /// <value>onrequest</value>
        /// <value>unknown</value>
        public string stock_delivery_type { get; set; }

        /// <summary>
        /// This field will be showed as the standard delivery text
        /// </summary>
        public string stock_delivery_standard { get; set; }

        /// <summary>
        /// Show the product as a offer at the beginpage or offer element
        /// Required
        /// </summary>
        public bool? show_on_beginpage { get; set; }

        /// <summary>
        /// Show the product in the facebook shop when available
        /// Required
        /// </summary>
        public bool? show_on_facebook { get; set; }

        /// <summary>
        /// Show order/offer button.
        /// Y = show
        /// QUOTATION = offer button
        /// N = No button
        /// </summary>
        /// <value>Y</value>
        /// <value>QUOTATION</value>
        /// <value>N</value>
        public string show_order_button { get; set; }

        /// <summary>
        /// Product Layout, value between 1-4
        /// 1=Standard layout,
        /// 2=Quick order,
        /// 3=Split layout,
        /// 4=Tab Layout
        /// Required
        /// </summary>
        public int? product_layout { get; set; }

        /// <summary>
        /// The photo description size
        /// </summary>
        /// <value>NONE</value>
        /// <value>SMALL</value>
        /// <value>BIG</value>
        public string photo_size { get; set; }

        /// <summary>
        /// Hide the products without categories.
        /// YES=Hide,
        /// NO_DIRECTLINK=No, the product can be found by direct url,
        /// NO_SEARCHRESULTS = No, Product can be found in the searchresults
        /// </summary>
        /// <value>YES</value>
        /// <value>NO_DIRECTLINK</value>
        /// <value>NO_SEARCHRESULTS</value>
        public string hide_without_category { get; set; }

        /// <summary>
        /// Internal memo for internal purpose only
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// Enable / disable Marktplaats.
        /// </summary>
        public bool? marktplaats_active { get; set; }

        /// <summary>
        /// The status for this advertisement
        /// </summary>
        /// <value>ACTIVE</value>
        /// <value>PAUSED</value>
        /// <value>DELETED</value>
        public string marktplaats_status { get; set; }

        /// <summary>
        /// Cost per click in euro cents, value between 1-250
        /// </summary>
        public double? marktplaats_cpc { get; set; }

        /// <summary>
        /// Daily budget for this advertisement in euro cents, minimum: 10
        /// </summary>
        public double? marktplaats_daily_budget { get; set; }

        /// <summary>
        /// Total budget for this advertisement in euro cents. You can use 0 euro cents for a infinite total budget or 5000 euro cents as minimum.
        /// </summary>
        public double? marktplaats_total_budget { get; set; }

        /// <summary>
        /// The Marktplaats category Id, min: 1
        /// </summary>
        public int? marktplaats_category_id { get; set; }

        /// <summary>
        /// The price type
        /// </summary>
        /// <value>FIXED PRICE</value>
        /// <value>NEGOTIABLE</value>
        /// <value>ON DEMAND</value>
        public string marktplaats_price_type { get; set; }

        //TODO: package
        //TODO: supplier

        /// <summary>
        /// Is this product included in the export feed
        /// </summary>
        public bool? is_included_for_export_feed { get; set; }

        /// <summary>
        /// Fixed staggered prices enabled
        /// </summary>
        public bool? fixed_staggered_prices { get; set; }
    }
}
