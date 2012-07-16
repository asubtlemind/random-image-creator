using System;
using System.Drawing;

namespace CreateRandomImage
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			for (int i = 0; i < 10; i++) 
			{

				Console.WriteLine ("setting up random image " + i.ToString());
				RandomImage ri = new RandomImage (128, 96);

				Console.WriteLine ("drawing image and saving ...");
				ri.fillImage ();
				ri.saveImageToJPEG (i.ToString() + "randomimage.jpg");

			}
			Console.WriteLine ("done");
		}
	}
}
