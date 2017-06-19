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
        public const string ITEM_GET = "Category_Get";
        public const string ITEM_CREATE = "Item_Create";
        public const string ITEM_UPDATE = "Item_Update";
        public const string ITEM_DELETE = "Item_Delete";
        #endregion

        #region Image
        public const string IMAGE_GET_BY_ID = "Image_Get_By_Id";
        public const string IMAGE_CREATE = "Image_Create";
        public const string IMAGE_DELETE = "Image_Delete";
        #endregion
    }
}