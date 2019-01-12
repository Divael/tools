using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace Comport
{
	public class Common
	{
		public static string[] GetPortName()
		{
			return SerialPort.GetPortNames();
		}

		public static int GetIndex(List<byte> ReadBytes, byte[] Flag, bool Front)
		{
			int result = -1;
			if (Front)
			{
				for (int i = 0; i < ReadBytes.Count; i++)
				{
					int j = 0;
					while (j < Flag.Length && i + j <= ReadBytes.Count && ReadBytes[i + j] == Flag[j])
					{
						j++;
					}
					if (j == Flag.Length)
					{
						result = i;
						break;
					}
				}
			}
			else
			{
				for (int i = ReadBytes.Count - 1; i > -1; i--)
				{
					int j;
					for (j = Flag.Length - 1; j > -1; j--)
					{
						byte b = ReadBytes[i];
						byte b2 = Flag[j];
					}
					if (j == 0)
					{
						result = i;
						break;
					}
				}
			}
			return result;
		}



    }
}
