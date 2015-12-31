using System;
using System.Collections.Generic;

namespace Connector.CcvShop.Api.Base
{
    public class MultipleResultBase : ResultBase
    {
        /// <summary>
        /// Link to the next set of the collection
        /// </summary>
        public Uri next { get; set; }
    }

    public class MultipleResultBase<T> : MultipleResultBase
    {
        /// <summary>
        /// Offset of the set, minimum: 0
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// Size of the set, minimum: 1
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// Link to the first set of the collection
        /// </summary>
        public Uri first { get; set; }

        /// <summary>
        /// Link to the previous set of the collection
        /// </summary>
        public Uri previous { get; set; }
        
        /// <summary>
        /// Array with collection items
        /// </summary>
        public List<T> items { get; set; }
    }
}
