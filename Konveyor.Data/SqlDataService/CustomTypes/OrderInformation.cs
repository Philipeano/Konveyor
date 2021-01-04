using Konveyor.Core.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Konveyor.Data.SqlDataService.CustomTypes
{
    public struct OrderInformation : IEquatable<OrderInformation>
    {
        public bool Equals([AllowNull] OrderInformation orderInfo)
        {
            throw new NotImplementedException();
        }


        private Orders order;

        private OrderUpdates initialUpdate;

        private OrderUpdates recentUpdate;


        public Orders Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }


        public OrderUpdates InitialUpdate
        {
            get
            {
                return initialUpdate;
            }

            set
            {
                initialUpdate = value;
            }
        }


        public OrderUpdates RecentUpdate
        {
            get
            {
                return recentUpdate;
            }

            set
            {
                recentUpdate = value;
            }
        }
    }
}

