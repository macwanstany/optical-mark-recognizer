using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge;
using System.Windows.Forms;
using AForge.Imaging.Filters;

namespace Nova_Optical_Mark_Recognizer
{
    /// <summary>
    /// Provides various OMR processing functions - Sheet Extraction, etc
    /// </summary>
    public static class OmrProcessor
    {
        public static Bitmap ExtractPaperFromFlattened(Bitmap bitmap, Bitmap originalimage, int minblobwidth, int minblobheight, bool applyrotation)
        {
            Bitmap bm = new Bitmap(bitmap.Size.Width, bitmap.Size.Height);
            Graphics g = Graphics.FromImage(bm);
            g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            Pen redpen = new Pen(Color.Red, 2);

            AForge.Math.Geometry.SimpleShapeChecker detectshape = new AForge.Math.Geometry.SimpleShapeChecker();

            if (applyrotation)
            {
                //rotate the image in case it is not properly oriented
                // lock the image
                BitmapData bitmapdata_rot = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite, bitmap.PixelFormat);

                BlobCounter blobCounter_rot = new BlobCounter();

                blobCounter_rot.FilterBlobs = true;
                blobCounter_rot.MinHeight = minblobheight;
                blobCounter_rot.MinWidth = minblobwidth;

                blobCounter_rot.ProcessImage(bitmapdata_rot);
                bitmap.UnlockBits(bitmapdata_rot);

                Blob[] blob_objects_rot = blobCounter_rot.GetObjects(bitmap, false);
                
                try
                {
                    foreach (Blob blob in blob_objects_rot)
                    {
                        List<IntPoint> edgePoints = blobCounter_rot.GetBlobsEdgePoints(blob);

                        //detect rotated paper based on left edge rectangular mark                                        
                        List<IntPoint> cornerPoints;
                        if (detectshape.IsQuadrilateral(edgePoints, out cornerPoints))
                        {
                            if (detectshape.CheckPolygonSubType(cornerPoints) == AForge.Math.Geometry.PolygonSubType.Rectangle)
                            {
                                if ((blob.Fullness > 0.7) && (((double)blob.Image.Width / (double)bitmap.Size.Width > 0.025) && ((double)blob.Image.Width / (double)bitmap.Size.Width < 0.037)) && (((double)blob.Image.Height / (double)bitmap.Size.Height > 0.005) && ((double)blob.Image.Height / (double)bitmap.Size.Height < 0.013)))
                                {
                                    // A------p------B      Suppose these are the Coordinates of the Rectangle (image) 
                                    // |  2   |  1   |      A,B,C,D are vertices and 1,2,3,4 are quadrants
                                    // s------O------q      p,q,r,s midpoints
                                    // |  3   |  4   |      O is the center of the rectangle
                                    // D------r------C      let the blob be denoted by BL

                                    System.Drawing.Point cent_O = new System.Drawing.Point(bitmap.Size.Width / 2, bitmap.Size.Height / 2);

                                    GraphicsUnit units = GraphicsUnit.Point;
                                    System.Drawing.Point pnt_A = new System.Drawing.Point((int)bitmap.GetBounds(ref units).X, (int)bitmap.GetBounds(ref units).Y);
                                    System.Drawing.Point pnt_B = new System.Drawing.Point(bitmap.Size.Width, (int)bitmap.GetBounds(ref units).Y);
                                    System.Drawing.Point pnt_C = new System.Drawing.Point(bitmap.Size.Width, bitmap.Size.Height);
                                    System.Drawing.Point pnt_D = new System.Drawing.Point((int)bitmap.GetBounds(ref units).X, bitmap.Size.Height);

                                    System.Drawing.Point midpnt_P = new System.Drawing.Point((pnt_A.X + pnt_B.X) / 2, (pnt_A.Y + pnt_B.Y) / 2);
                                    System.Drawing.Point midpnt_Q = new System.Drawing.Point((pnt_B.X + pnt_C.X) / 2, (pnt_B.Y + pnt_C.Y) / 2);
                                    System.Drawing.Point midpnt_R = new System.Drawing.Point((pnt_C.X + pnt_D.X) / 2, (pnt_C.Y + pnt_D.Y) / 2);
                                    System.Drawing.Point midpnt_S = new System.Drawing.Point((pnt_A.X + pnt_D.X) / 2, (pnt_A.Y + pnt_D.Y) / 2);

                                    System.Drawing.Point blob_CENGRAV = new System.Drawing.Point((int)blob.CenterOfGravity.X, (int)blob.CenterOfGravity.Y);

                                    if (ImageProcessor.IsPointInsideRegion(bitmap, pnt_A, midpnt_P, cent_O, midpnt_S, blob_CENGRAV))
                                    {
                                        //do nothing...image is properly oriented
                                        break; //terminate the loop
                                    }
                                    else if (ImageProcessor.IsPointInsideRegion(bitmap, midpnt_P, pnt_B, midpnt_Q, cent_O, blob_CENGRAV))
                                    {
                                        //blob is present in quadrant 1
                                        //rotate image by three CW
                                        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        originalimage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        break;
                                    }
                                    else if (ImageProcessor.IsPointInsideRegion(bitmap, cent_O, midpnt_Q, pnt_C, midpnt_R, blob_CENGRAV))
                                    {
                                        //blob is present in quadrant 4
                                        //rotate image by two CW
                                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                        originalimage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                        break;
                                    }
                                    else if (ImageProcessor.IsPointInsideRegion(bitmap, cent_O, midpnt_R, pnt_D, midpnt_S, blob_CENGRAV))
                                    {
                                        //blob is present in quadrant 3
                                        //rotate image by one CW
                                        bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                        originalimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (ArgumentException) { MessageBox.Show("Bilinear Rotational Transformation Failed"); }
            }
            //After applying rotational process, extract the blobs once again to find the circular edge markers

            // lock the image
            BitmapData bitmapdata = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // Finding Blobs in the Bitmap image
            BlobCounter blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = minblobheight;
            blobCounter.MinWidth = minblobwidth;

            blobCounter.ProcessImage(bitmapdata);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            bitmap.UnlockBits(bitmapdata);

            //Rectangle[] rects = blobCounter.GetObjectsRectangles();
            Blob[] blob_objects = blobCounter.GetObjects(bitmap, false);

            // Paper Detection happens through the detection of the edge-markers placed on all four 
            // edges of the OMR sheet                                

            List<IntPoint> quad = new List<IntPoint>(); // Store sheet corner locations (if anyone is detected )

            try
            {
                foreach (Blob blob in blob_objects)
                {
                    //detect edge circles                
                    if ((double)blob.Rectangle.Width / blob.Rectangle.Height < 1.4 &&
                        (double)blob.Rectangle.Width / blob.Rectangle.Height > .6) // filters out blobs having insanely wrong aspect ratio
                    {
                        List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blob);

                        //detect circles only                                                             
                        if (detectshape.IsCircle(edgePoints))
                        {
                            //detect filled circles only
                            //the ratio of width(blob)/width(bitmap) should be in range of 0.031 - 0.042
                            if ((blob.Fullness > 0.7) && (((double)blob.Image.Width / (double)bitmap.Width) > 0.031) && (((double)blob.Image.Width / (double)bitmap.Width) < 0.042))
                            {
                                //g.DrawRectangle(redpen, blob.Rectangle);
                                quad.Add(new IntPoint((int)blob.CenterOfGravity.X, (int)blob.CenterOfGravity.Y));
                            }
                        }
                    }
                }

                // filter out if wrong blobs pretend to be our blobs.
                if (quad.Count == 4)
                {
                    if (!((quad[0].DistanceTo(quad[1]) / quad[0].DistanceTo(quad[2]) > 0.5) &&
                        (quad[0].DistanceTo(quad[1]) / quad[0].DistanceTo(quad[2]) < 1.5)))
                    {
                        quad.Clear();
                    }
                    else
                    {
                        //rearrange the edge coordinates, if not in proper manner        
                        while ((quad[0].X > quad[1].X) || (quad[1].Y > quad[3].Y) || (quad[2].X > quad[3].X) || (quad[0].Y > quad[2].Y))
                        {
                            if (quad[0].X > quad[1].X)
                            {
                                IntPoint tmp = quad[0];
                                quad[0] = quad[1];
                                quad[1] = tmp;
                            }

                            if (quad[1].Y > quad[3].Y)
                            {
                                IntPoint tmp = quad[1];
                                quad[1] = quad[3];
                                quad[3] = tmp;
                            }

                            if (quad[2].X > quad[3].X)
                            {
                                IntPoint tmp = quad[3];
                                quad[3] = quad[2];
                                quad[2] = tmp;
                            }

                            if (quad[0].Y > quad[2].Y)
                            {
                                IntPoint tmp = quad[0];
                                quad[0] = quad[2];
                                quad[2] = tmp;
                            }
                        }
                    }
                }

                if (quad.Count != 4)
                {
                    //call recursively as the sheet is not detected properly
                    //todo: try altering the blob height and width, threshold etc, when program does not find edge markers 
                }
                else
                {
                    //rearrange the edges coordinates

                    //  0----1
                    //  |    |
                    //  2----3

                    //interchange 2 and 3 
                    IntPoint tp2 = quad[3];
                    quad[3] = quad[2];
                    quad[2] = tp2;

                    //sort the edges for wrap operation
                    QuadrilateralTransformation wrap = new QuadrilateralTransformation(quad, 2100, 2970);
                    wrap.UseInterpolation = false;
                    wrap.AutomaticSizeCalculaton = true;                    
                    Bitmap wrappedoriginal = wrap.Apply(originalimage);
                    return wrappedoriginal;
                }
            }
            catch (ArgumentException) { MessageBox.Show("No Blobs Found"); }
            return null;
        }

        /// <summary>
        /// Applies blob Processing on the image and extracts blobs corresponding to the bubbles and return them as a blob array.
        /// <param name="bitmap">Pre Processed Image</param>
        /// </summary>
        public static List<Blob> ExtractBubbleCorrespondingBlobs(Bitmap bitmap, int minblobwidth, int minblobheight)
        {
            Bitmap bm = new Bitmap(bitmap.Size.Width, bitmap.Size.Height);
            Graphics g = Graphics.FromImage(bm);
            g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            Pen redpen = new Pen(Color.Red, 3);

            AForge.Math.Geometry.SimpleShapeChecker detectshape = new AForge.Math.Geometry.SimpleShapeChecker();

            // lock the image
            BitmapData bitmapdata = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // Finding Blobs in the Bitmap image
            BlobCounter blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = minblobheight;
            blobCounter.MinWidth = minblobwidth;

            blobCounter.ProcessImage(bitmapdata);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            bitmap.UnlockBits(bitmapdata);

            //Rectangle[] rects = blobCounter.GetObjectsRectangles();
            Blob[] blob_objects = blobCounter.GetObjects(bitmap, false);

            List<Blob> bubblesblobs = new List<Blob>();
            
            try
            {
                foreach (Blob blob in blob_objects)
                {
                    //detect circles through aspect ratio               
                    if ((double)blob.Rectangle.Width / blob.Rectangle.Height < 1.4 &&
                        (double)blob.Rectangle.Width / blob.Rectangle.Height > .6) // filters out blobs having insanely wrong aspect ratio
                    {
                        List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blob);
                        detectshape.MinAcceptableDistortion = 0.5f; //0.5f;
                        detectshape.RelativeDistortionLimit = 0.079f; //0.057f;

                        AForge.Point center;
                        float radius;

                        //detect circles only                                                                              
                        if (detectshape.IsCircle(edgePoints, out center, out radius))
                        {
                            //detect filled bubbles only
                            if ((blob.Fullness > 0.60) && ((double)blob.Image.Width/(double)bitmap.Size.Width > 0.014))
                            {
                                //g.DrawEllipse(redpen,
                                //    (int)(center.X - radius),
                                //    (int)(center.Y - radius),
                                //    (int)(radius * 2),
                                //    (int)(radius * 2));
                                bubblesblobs.Add(blob);
                            }
                        }
                    }
                }
                return bubblesblobs;
            }
            catch (ArgumentException) { MessageBox.Show("Bubble Detection Failed"); }
            return null;
        
        }

    }
}
