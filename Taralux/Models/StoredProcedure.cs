using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public static class StoredProcedure
    {
        #region Category
        public const string CATEGORY_GET = "Category_Get";
        public const string CATEGORY_CREATE = "Category_Create";
        public const string CATEGORY_UPDATE = "Category_Update";
        public const string CATEGORY_DELETE = "Category_Delete";
        #endregion

        #region Item
        public const string ITEM_GET = "Item_Get";
        public const string ITEM_CREATE = "Item_Create";
        public const string ITEM_UPDATE = "Item_Update";
        public const string ITEM_DELETE = "Item_Delete";
        #endregion

        #region Image
        public const string IMAGE_GET_BY_ID = "Image_Get_By_Id";
        public const string IMAGE_CREATE = "Image_Create";
        public const string IMAGE_DELETE = "Image_Delete";
        #endregion

        #region Electrician
        public const string ELECTRICIAN_GET = "Electrician_Get";
        public const string ELECTRICIAN_CREATE = "Electrician_Create";
        public const string ELECTRICIAN_UPDATE = "Electrician_Update";
        public const string ELECTRICIAN_DELETE = "Electrician_Delete";
        #endregion
    }
}