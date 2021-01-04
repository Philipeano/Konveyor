using Konveyor.Core.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Konveyor.Data.SqlDataService.CustomTypes
{
    public struct PackageInformation : IEquatable<PackageInformation>
    {
        public bool Equals([AllowNull] PackageInformation packageInfo)
        {
            throw new NotImplementedException();
        }


        private Packages package;

        private PackageUpdates initialUpdate;

        private PackageUpdates recentUpdate;


        public Packages Package
        {
            get
            {
                return package;
            }

            set
            {
                package = value;
            }
        }


        public PackageUpdates InitialUpdate
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


        public PackageUpdates RecentUpdate
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

