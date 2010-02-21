using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeathSquid
{
	class ShooterLevelNew
	{
		private List<ShooterHerdNew> _herdList;

		public ShooterLevelNew()
		{
			_herdList = new List<ShooterHerdNew>();
		}

		public void AddHerd(ShooterHerdNew h)
		{
			_herdList.Add(h);
		}

		public List<ShooterHerdNew> GetHerdList()
		{
			return _herdList;
		}
	}
}