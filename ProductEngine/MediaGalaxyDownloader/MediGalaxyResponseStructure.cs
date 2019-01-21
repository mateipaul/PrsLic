using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaGalaxyDownloader
{
    public class Search
    {
        public string term { get; set; }
    }

    public class Meta
    {
        public int items { get; set; }
        public int size { get; set; }
        public int page { get; set; }
        public int offset { get; set; }
        public string requestId { get; set; }
        public string sort_direction { get; set; }
        public string sort_attribute { get; set; }
    }

    public class AltexLabelRuleActionTypeText
    {
        public string positions { get; set; }
        public string values { get; set; }
    }

 

public class AltexLabelRuleActionTypeCmsBlockBottom
{
    public string positions { get; set; }
    public string values { get; set; }
}

public class AltexLabelRuleActionTypeCmsBlock
{
    public string positions { get; set; }
    public string values { get; set; }
}




public class Product
{
    public string short_description { get; set; }
    public string image { get; set; }
    public string thumbnail { get; set; }
    public int stock_status { get; set; }
    public int preorder_status { get; set; }
    public int visibility { get; set; }
    public string small_image { get; set; }
    public string type_id { get; set; }
    public int resealed_count { get; set; }
    public string brand_name { get; set; }
    public string discount_type { get; set; }
    public string url_key { get; set; }
    public int on_demand { get; set; }
    public double regular_price { get; set; }
    public List<int> seller_ids { get; set; }
    public int attribute_set_id { get; set; }
    public int pickup_is_in_stock { get; set; }
    public double price { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public string sku { get; set; }
    public int brand { get; set; }
    public int status { get; set; }
    public int eol_status { get; set; }
    public int is_eol { get; set; }
    public int is_resealed { get; set; }
    public int reviews_count { get; set; }
    public int reviews_value { get; set; }
    public int questions_count { get; set; }
    public int has_gift { get; set; }
    public string ean_codes { get; set; }
    public string preorder_release_date { get; set; }
}

public class Filter
{
    public object value { get; set; }
    public string label { get; set; }
    public string slug { get; set; }
    public int count { get; set; }
    public int? order { get; set; }
    public string attributeSlug { get; set; }
}

public class LayeredNavigation
{
    public string label { get; set; }
    public string attribute_code { get; set; }
    public int order { get; set; }
    public string slug { get; set; }
    public List<Filter> filters { get; set; }
    public bool? hasAttributeSlug { get; set; }
}

public class MediGalaxyResponseStructure
{
    public Search search { get; set; }
    public Meta meta { get; set; }
    public List<Product> products { get; set; }
    public List<LayeredNavigation> layeredNavigation { get; set; }
    }

}
