using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace CreateRandomImage
{
	public class RandomImage
	{
		private Bitmap imageBitmap;
		private Random r;
		private byte pixelChange = 1;

		public RandomImage (int width, int height)
		{
			imageBitmap = new Bitmap(width, height);
			r = new Random();
		}

		public RandomImage (int width, int height, int seed)
		{
			imageBitmap = new Bitmap(width, height);
			r = new Random(seed);
		}

		public void setPixelChange (byte change)
		{
			pixelChange = change;
		}

		private Color getPreviousPixel (int x, int y)
		{
			if (x == 0 && y > 0) {
				return this.imageBitmap.GetPixel (x, y - 1);
			} else if (y == 0) {
				return this.imageBitmap.GetPixel(x - 1, y);
			} else {
				double randDbl = r.NextDouble();

				if( randDbl < 1 )
					return this.imageBitmap.GetPixel(x-1,y);
				else if( randDbl < .5 )
					return this.imageBitmap.GetPixel(x-1,y-1);
				else if( randDbl < .75 )
					return this.imageBitmap.GetPixel(x,y-1);
				else
					if(y < this.imageBitmap.Height - 1)
						return this.imageBitmap.GetPixel(x-1,y+1);
					else
						return this.imageBitmap.GetPixel(x,y-1);
			}
		}

		private Color changeNewPixel(byte red, byte green, byte blue)
		{

			double randDbl = r.NextDouble();

			if( randDbl < .165 )
				red += pixelChange;
			if( randDbl < .33 )
				red -= pixelChange;
			if( randDbl < .495 )
				green += pixelChange;
			if( randDbl < .655 )
				green -= pixelChange;
			if( randDbl < .815 )
				blue += pixelChange;
			if( randDbl < .975 )
				blue -= pixelChange;

			setPixelChange((byte) (r.Next() % 32));

			return Color.FromArgb(red,green,blue);
		}

		public void fillPixel (int x, int y)
		{
			byte red;
			byte green;
			byte blue;
			Color randomColor;

			double randDbl = r.NextDouble();

			if ((x > 0 && y >= 0) && (randDbl < .99)) {
				Color prevPixel = getPreviousPixel (x, y);
				randomColor = changeNewPixel (prevPixel.R, prevPixel.G, prevPixel.B);
			} 
			else  {
				red = (byte)(r.Next () % 256);
				green = (byte)(r.Next () % 256);
				blue = (byte)(r.Next () % 256);
				randomColor = Color.FromArgb (red, green, blue);
			}

			imageBitmap.SetPixel(x,y, randomColor);
		}

		public void fillImage ()
		{
			for (int x = 0; x < this.imageBitmap.Width; x++) 
				for( int y = 0; y < this.imageBitmap.Height; y++)
					fillPixel(x,y);
		}

		private static ImageCodecInfo GetEncoderInfo(String mimeType)
	    {
	        ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
	        for(int j = 0; j < encoders.Length; ++j)
	        {
	            if(encoders[j].MimeType == mimeType)
	                return encoders[j];
	        }
	        return null;
	    }

		public void saveImageToJPEG(string imageName)
		{
			Encoder myEncoder = Encoder.Quality;
			EncoderParameter myEncoderParameter; 
        	EncoderParameters myEncoderParameters = new EncoderParameters(1);
			ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");

			myEncoderParameter = new EncoderParameter(myEncoder, 100L);
        	myEncoderParameters.Param[0] = myEncoderParameter;
        	imageBitmap.Save(imageName, myImageCodecInfo, myEncoderParameters);
		}
	}
}

