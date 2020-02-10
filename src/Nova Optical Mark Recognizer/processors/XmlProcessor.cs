using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge;
using AForge.Imaging;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace Nova_Optical_Mark_Recognizer
{
    /// <summary>
    /// Reader and Writer for XML
    /// </summary>
    public static class XMLReaderWriter
    {
        /// <summary>
        /// Creates a new XML file containing detected bubbles information
        /// <param name="bubblesblobs">Blob list</param>
        /// <param name="filename">Name with which the XML should be saved</param>
        /// </summary>
        public static void WriteNewBubblesDataXML(List<Blob> bubblesblobs, string filename)
        {
            try
            {               
                XmlDocument xmldoc = new XmlDocument();
                XmlNode rootnode = xmldoc.CreateElement("BubbleCollection");
                xmldoc.AppendChild(rootnode);

                foreach (Blob bubbleblob in bubblesblobs)
                {
                    XmlNode bubblenode = xmldoc.CreateElement("Bubble");
                    XmlAttribute bubbleidattr = xmldoc.CreateAttribute("id");
                    bubbleidattr.Value = bubbleblob.ID.ToString();
                    bubblenode.Attributes.Append(bubbleidattr);
                    rootnode.AppendChild(bubblenode);

                    XmlNode areanode = xmldoc.CreateElement("Area");
                    areanode.InnerText = bubbleblob.Area.ToString();
                    bubblenode.AppendChild(areanode);
                    
                    XmlNode centreofgravnode = xmldoc.CreateElement("CenterOfGravity");                    
                    bubblenode.AppendChild(centreofgravnode);                    
                    
                    XmlNode cog_x_node = xmldoc.CreateElement("X");
                    cog_x_node.InnerText = bubbleblob.CenterOfGravity.X.ToString();
                    centreofgravnode.AppendChild(cog_x_node);                    
                    
                    XmlNode cog_y_node = xmldoc.CreateElement("Y");
                    cog_y_node.InnerText = bubbleblob.CenterOfGravity.Y.ToString();
                    centreofgravnode.AppendChild(cog_y_node);

                    XmlNode fullnessnode = xmldoc.CreateElement("Fullness");
                    fullnessnode.InnerText = bubbleblob.Fullness.ToString();
                    bubblenode.AppendChild(fullnessnode);
                    
                    XmlNode rectanglenode = xmldoc.CreateElement("Rectangle");
                    bubblenode.AppendChild(rectanglenode);

                    XmlNode rect_Xnode = xmldoc.CreateElement("X");
                    rect_Xnode.InnerText = bubbleblob.Rectangle.X.ToString();
                    rectanglenode.AppendChild(rect_Xnode);

                    XmlNode rect_Ynode = xmldoc.CreateElement("Y");
                    rect_Ynode.InnerText = bubbleblob.Rectangle.Y.ToString();
                    rectanglenode.AppendChild(rect_Ynode);

                    XmlNode rect_heightnode = xmldoc.CreateElement("Height");
                    rect_heightnode.InnerText = bubbleblob.Rectangle.Height.ToString();
                    rectanglenode.AppendChild(rect_heightnode);

                    XmlNode rect_widthnode = xmldoc.CreateElement("Width");
                    rect_widthnode.InnerText = bubbleblob.Rectangle.Width.ToString();
                    rectanglenode.AppendChild(rect_widthnode);

                    XmlNode rect_topnode = xmldoc.CreateElement("Top");
                    rect_topnode.InnerText = bubbleblob.Rectangle.Top.ToString();
                    rectanglenode.AppendChild(rect_topnode);

                    XmlNode rect_bottomnode = xmldoc.CreateElement("Bottom");
                    rect_bottomnode.InnerText = bubbleblob.Rectangle.Bottom.ToString();
                    rectanglenode.AppendChild(rect_bottomnode);

                    XmlNode rect_leftnode = xmldoc.CreateElement("Left");
                    rect_leftnode.InnerText = bubbleblob.Rectangle.Left.ToString();
                    rectanglenode.AppendChild(rect_leftnode);

                    XmlNode rect_rightnode = xmldoc.CreateElement("Right");
                    rect_rightnode.InnerText = bubbleblob.Rectangle.Right.ToString();
                    rectanglenode.AppendChild(rect_rightnode);

                    XmlNode rect_loc_node = xmldoc.CreateElement("Location");
                    rectanglenode.AppendChild(rect_loc_node);

                    XmlNode rect_loc_Xnode = xmldoc.CreateElement("X");
                    rect_loc_Xnode.InnerText = bubbleblob.Rectangle.Location.X.ToString();
                    rect_loc_node.AppendChild(rect_loc_Xnode);

                    XmlNode rect_loc_Ynode = xmldoc.CreateElement("Y");
                    rect_loc_Ynode.InnerText = bubbleblob.Rectangle.Location.Y.ToString();
                    rect_loc_node.AppendChild(rect_loc_Ynode);

                    XmlNode rect_size_node = xmldoc.CreateElement("Size");
                    rectanglenode.AppendChild(rect_size_node);

                    XmlNode rect_size_heightnode = xmldoc.CreateElement("Height");
                    rect_size_heightnode.InnerText = bubbleblob.Rectangle.Size.Height.ToString();
                    rect_size_node.AppendChild(rect_size_heightnode);

                    XmlNode rect_size_widthnode = xmldoc.CreateElement("Width");
                    rect_size_widthnode.InnerText = bubbleblob.Rectangle.Size.Width.ToString();
                    rect_size_node.AppendChild(rect_size_widthnode);
                }

                xmldoc.Save(filename);                
            }
            catch(XmlException ex)
            {
                throw ex;
            }            
        }


        //public static Rectangle GetSheetPropertyLocation(string file, OMREnums.OMRSheet sheet, OMREnums.OMRProperty property)
        //{
        //    Rectangle rect = new Rectangle(
        //    Convert.ToInt32(XML.XMLReaderWriter.ReadValueD2(
        //        file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2),
        //        property.ToString(), "X")),
        //        Convert.ToInt32(XML.XMLReaderWriter.ReadValueD2(
        //        file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2),
        //        property.ToString(), "Y")),
        //        Convert.ToInt32(XML.XMLReaderWriter.ReadValueD2(
        //        file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2),
        //        property.ToString(), "Width")),
        //        Convert.ToInt32(XML.XMLReaderWriter.ReadValueD2(
        //        file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2),
        //        property.ToString(), "Height")));
        //    return rect;
        //}
    }

    /// <summary>
    /// OMR Sheet Specifications Writer Class 
    /// </summary>
    public static class OMRSheetSpecsWriter
    {
        /// <summary>
        /// Generates a new sheet specifications XML file 
        /// <param name="file">The name with which the file should be saved</param>
        /// </summary>
        public static void GenerateDefaultSheetSpecsFile(string file)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(file, settings);
            writer.WriteStartDocument();

            #region A4 Specifications
            writer.WriteStartElement("SheetSpecifications");
            
            writer.WriteStartElement("OmrSheet");
            writer.WriteAttributeString("sheetid", "1");
            writer.WriteAttributeString("sheetsize", "A4");
            writer.WriteAttributeString("totalquestions", "100");


            writer.WriteStartElement("BlobsConfig");
            writer.WriteElementString("MinBlobRatio", "0.6");
            writer.WriteElementString("MaxBlobRatio", "1.4");
            writer.WriteElementString("MinBlobHeight", "30");
            writer.WriteElementString("MinBlobwidth", "30");
            writer.WriteElementString("Fullness", "0.7");

            writer.WriteStartElement("BlobRectMarker");
            writer.WriteElementString("MinArea", "1250");
            writer.WriteElementString("MaxArea", "3110");
            writer.WriteEndElement();

            writer.WriteStartElement("BlobEdgeMarkers");
            writer.WriteElementString("MinWidthBlobtoDocRatio", "0.031");
            writer.WriteElementString("MaxWidthBlobtoDocRatio", "0.042");
            writer.WriteEndElement();

            writer.WriteStartElement("QuadTransform");
            writer.WriteElementString("NewWidth", "2100");
            writer.WriteElementString("NewHeight", "2970");
            writer.WriteEndElement();

            writer.WriteStartElement("BlobBubbles");
            writer.WriteElementString("MinAcceptableDistortion", "0.5");
            writer.WriteElementString("RelativeDistortionLimit", "0.057");
            writer.WriteElementString("Fullness", "0.65");
            writer.WriteEndElement();

            writer.WriteStartElement("PreProcess");            
            writer.WriteStartElement("Threshhold");
            writer.WriteAttributeString("order", "1");
            writer.WriteString("160");
            writer.WriteEndElement();
            writer.WriteStartElement("Threshhold");
            writer.WriteAttributeString("order", "2");
            writer.WriteString("190");
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteEndElement(); //</blobconfig>

            writer.WriteStartElement("AnsBlocksSpecs");
            writer.WriteElementString("X", "105");
            writer.WriteElementString("Y", "1365");
            writer.WriteElementString("Width", "2060");
            writer.WriteElementString("Height", "1866");
            writer.WriteElementString("NumberOfBlocks", "4");
            writer.WriteElementString("QuesPerBlock", "25");

            writer.WriteStartElement("BlockRect");
            writer.WriteAttributeString("tag", "1");
            writer.WriteElementString("X", "191");
            writer.WriteElementString("Y", "1471");
            writer.WriteElementString("Width", "365");
            writer.WriteElementString("Height", "1748");
            writer.WriteEndElement();

            writer.WriteStartElement("BlockRect");
            writer.WriteAttributeString("tag", "2");
            writer.WriteElementString("X", "724");
            writer.WriteElementString("Y", "1471");
            writer.WriteElementString("Width", "365");
            writer.WriteElementString("Height", "1748");
            writer.WriteEndElement();

            writer.WriteStartElement("BlockRect");
            writer.WriteAttributeString("tag", "3");
            writer.WriteElementString("X", "1258");
            writer.WriteElementString("Y", "1471");
            writer.WriteElementString("Width", "365");
            writer.WriteElementString("Height", "1748");
            writer.WriteEndElement();

            writer.WriteStartElement("BlockRect");
            writer.WriteAttributeString("tag", "4");
            writer.WriteElementString("X", "1797");
            writer.WriteElementString("Y", "1471");
            writer.WriteElementString("Width", "365");
            writer.WriteElementString("Height", "1748");
            writer.WriteEndElement();

            writer.WriteEndElement(); //</AnswerBlockSpecs>

            writer.WriteStartElement("CandidateName");
            writer.WriteElementString("X", "103");
            writer.WriteElementString("Y", "226");
            writer.WriteElementString("Width", "867");
            writer.WriteElementString("Height", "179");
            writer.WriteEndElement();

            writer.WriteStartElement("ExaminationSubject");
            writer.WriteElementString("X", "103");
            writer.WriteElementString("Y", "445");
            writer.WriteElementString("Width", "867");
            writer.WriteElementString("Height", "179");
            writer.WriteEndElement();

            writer.WriteStartElement("TestDate");
            writer.WriteElementString("X", "205");
            writer.WriteElementString("Y", "670");
            writer.WriteElementString("Width", "767");
            writer.WriteElementString("Height", "116");
            writer.WriteEndElement();

            writer.WriteStartElement("TestTime");
            writer.WriteElementString("X", "435");
            writer.WriteElementString("Y", "868");
            writer.WriteElementString("Width", "327");
            writer.WriteElementString("Height", "182");
            writer.WriteEndElement();

            writer.WriteStartElement("CandidateSignature");
            writer.WriteElementString("X", "1606");
            writer.WriteElementString("Y", "799");
            writer.WriteElementString("Width", "564");
            writer.WriteElementString("Height", "255");
            writer.WriteEndElement();

            writer.WriteStartElement("SupervisorSignature");
            writer.WriteElementString("X", "1606");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "564");
            writer.WriteElementString("Height", "245");
            writer.WriteEndElement();

            writer.WriteStartElement("MarksObtained");
            writer.WriteElementString("X", "1603");
            writer.WriteElementString("Y", "226");
            writer.WriteElementString("Width", "567");
            writer.WriteElementString("Height", "227");
            writer.WriteEndElement();
            #endregion
            
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }

    public static class OMRSheetSpecsReader
    {
       

    }
}
