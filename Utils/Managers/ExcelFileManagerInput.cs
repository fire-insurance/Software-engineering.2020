﻿using Model;
using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;

namespace Utils.Managers
{
	/// <summary>
	/// Input part
	/// </summary>
	public static partial class ExcelFileManager
	{
		public static IEnumerable<Purchase> Read(string fileName)
		{
			List<Purchase> purchases = ReadExcel(fileName);

			foreach (var purchase in purchases)
			{
				yield return purchase;
			}
		}

		private static List<Purchase> ReadExcel(string fileName)
		{
			var res = new List<Purchase>();
			using var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
			using var reader = ExcelReaderFactory.CreateReader(stream);

			do
			{
				Purchase purchase = null;
				while (reader.Read())       //each Row
				{
					var obj = reader.GetValue(0);
					if (obj is DateTime dateTime && DataValidation.IsDateValid(dateTime))
					{
						AddPurchase(res, purchase);

						purchase = new Purchase(dateTime);
					}
					else
					{
						var item = ReadRow(reader);

						if (item != null && purchase != null)
							purchase.Add(item);
					}
				}

				AddPurchase(res, purchase);

			}
			while (reader.NextResult());    //each sheet

			return res;

		}

		private static void AddPurchase(List<Purchase> res, Purchase purchase)
		{
			if (purchase != null && purchase.ItemsAmount != 0)
				res.Add(purchase);
		}

		private static PurchaseItem ReadRow(IExcelDataReader reader)
		{
			try
			{
				int column = 0;
				var type = reader.GetString(column++);
				var name = reader.GetString(column++);

				var priceObj = reader.GetValue(column++);
				decimal price = 0;
				if (priceObj != null)
				{
					//price = (decimal)(double)priceObj;
					price = decimal.Parse(priceObj.ToString());
				}
				else
				{
					throw new ArgumentException($"Wrong input");
				}

				var amountObj = reader.GetValue(column++);
				int amount = 0;
				if (amountObj != null)
				{
					//amount = (int)(double)amountObj;
					amount = int.Parse(amountObj.ToString());
				}
				else
				{
					throw new ArgumentException($"Wrong input");
				}

				if (DataValidation.IsNameValid(type)
							&& DataValidation.IsNameValid(name)
							&& DataValidation.IsPriceValid(price)
							&& DataValidation.IsAmountValid(amount))
				{
					return new PurchaseItem(name, price, type, amount);
				}
				else
				{
					throw new ArgumentException($"Wrong input");
				}
			}
			catch
			{
				return null;
			}
		}

		
	}
}
