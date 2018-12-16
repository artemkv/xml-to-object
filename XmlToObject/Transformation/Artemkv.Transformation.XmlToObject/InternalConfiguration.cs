#region Copyright
// XmlToObject
// Copyright (C) 2012  Artem Kondratyev

// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
#endregion Copyright

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Internal cache expiration rates.
	/// </summary>
	public enum CacheExpirationRate
	{
		/// <summary>
		/// Fast cache expiration rate (30 seconds).
		/// </summary>
		Fast,
		/// <summary>
		/// Normal cache expiration rate (20 minutes).
		/// </summary>
		Normal,
		/// <summary>
		/// Slow cache expiration rate (1 hour).
		/// </summary>
		Slow
	}

	/// <summary>
	/// Provides access to internal parameters.
	/// </summary>
	public static class InternalConfiguration
	{
		/// <summary>
		/// The number of seconds that the object is cached at fast expiration rate.
		/// </summary>
		public static int FastCacheExpirationPeriodSeconds = 30;
		/// <summary>
		/// The number of seconds that the object is cached at normal expiration rate.
		/// </summary>
		public static int NormalCacheExpirationPeriodSeconds = 1200;
		/// <summary>
		/// The number of seconds that the object is cached at slow expiration rate.
		/// </summary>
		public static int SlowCacheExpirationPeriodSeconds = 3600;

		private static int _cacheExpirationPeriodSeconds = NormalCacheExpirationPeriodSeconds;
		private static CacheExpirationRate _cacheExpirationRate = CacheExpirationRate.Normal;

		/// <summary>
		/// Gets or sets the rate of the internal cache expiration.
		/// </summary>
		public static CacheExpirationRate CacheExpirationRate
		{
			get
			{
				return _cacheExpirationRate;
			}
			set
			{
				_cacheExpirationRate = value;
				switch (value)
				{
					case XmlToObject.CacheExpirationRate.Normal:
						_cacheExpirationPeriodSeconds = NormalCacheExpirationPeriodSeconds;
						break;
					case XmlToObject.CacheExpirationRate.Slow:
						_cacheExpirationPeriodSeconds = SlowCacheExpirationPeriodSeconds;
						break;
					case XmlToObject.CacheExpirationRate.Fast:
						_cacheExpirationPeriodSeconds = FastCacheExpirationPeriodSeconds;
						break;
					default:
						throw new InvalidOperationException();
				}
			}
		}

		internal static int CacheExpirationPeriodSeconds
		{
			get
			{
				return _cacheExpirationPeriodSeconds;
			}
		}
	}
}
