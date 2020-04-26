using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Utilities
{
    public class PdfWriter
    {
        public Document document { get; set; }
        public TextFrame addressFrame { get; set; }
        public Table table { get; set; }

        public void WritePdf(Runsheet runsheet)
        {
            CreateDocument(runsheet);
        }

        private void CreateDocument(Runsheet runsheet)
        {
            this.document = new Document();
            DefineStyle();
            CreatePage(runsheet);
            FillContent(runsheet);
            var render = new PdfDocumentRenderer();
            render.Document = document;
            render.RenderDocument();
            render.Save(
                @"C:\Users\alex_\source\repos\VehicleRunsheetMBA\VehicleRunsheetMBA\wwwroot\CsvFiles\OutputPdf.pdf");
        }

        private void DefineStyle()
        {
            // Get the predefined style Normal.
            Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Arial";
            style.Font.Size = 12;

            //// Create a new style called Reference based on style Normal
            //style = this.document.Styles.AddStyle("Reference", "Normal");
            //style.ParagraphFormat.SpaceBefore = "5mm";
            //style.ParagraphFormat.SpaceAfter = "5mm";
            //style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        private void CreatePage(Runsheet runsheet)
        {
            var section = this.document.AddSection();

            // Put a logo in the header
            var image = section.Headers.Primary.AddImage(
                @"C:\Users\alex_\source\repos\VehicleRunsheetMBA\VehicleRunsheetMBA\wwwroot\icon-512.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create the text frame for the Details
            addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "5.0cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            //// Put sender in address frame
            //var paragraph = addressFrame.AddParagraph($"Start: {runsheet.StartOdometer} · End: {runsheet.EndOdometer} · Driver: {runsheet.Driver} · Vehicle: {runsheet.VehicleDetails}");
            //paragraph.Format.Font.Name = "Times New Roman";
            //paragraph.Format.Font.Size = 7;
            //paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            var paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "10cm";
            paragraph.Style = "Reference";
            //paragraph.AddFormattedText("INVOICE", TextFormat.Bold);
            //paragraph.AddTab();
            //paragraph.AddText("Cologne, ");
            //paragraph.AddDateField("dd.MM.yyyy");


            // Create the item table
            table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            //Define Columns
            var column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            // Create the header of the table
            var row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("Date");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;

            row.Cells[1].AddParagraph("Start");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;

            row.Cells[2].AddParagraph("End");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;

            row.Cells[3].AddParagraph("Customer");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;

            row.Cells[4].AddParagraph("Order#");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;

            row.Cells[5].AddParagraph("Received By");
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;

            table.SetEdge(0, 0, 5, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }

        private void FillContent(Runsheet runsheet)
        {
            var para1 = addressFrame.AddParagraph();
            para1.Format.Font.Bold = true;
            para1.AddText("Start Odometer");
            para1.Format.LineSpacingRule = LineSpacingRule.OnePtFive;
            para1.AddLineBreak();

            var para2 = addressFrame.AddParagraph();
            para2.AddText(runsheet.StartOdometer.ToString());
            para2.Format.LineSpacingRule = LineSpacingRule.Single;
            para2.AddLineBreak();
            para2.AddLineBreak();

            var para3 = addressFrame.AddParagraph();
            para3.Format.Font.Bold = true;
            para3.AddText("End Odometer");
            para3.Format.LineSpacingRule = LineSpacingRule.OnePtFive;
            para3.AddLineBreak();

            var para4 = addressFrame.AddParagraph();
            para4.AddText(runsheet.EndOdometer.ToString());
            para4.Format.LineSpacingRule = LineSpacingRule.Single;
            para4.AddLineBreak();
            para4.AddLineBreak();

            var para5 = addressFrame.AddParagraph();
            para5.Format.Font.Bold = true;
            para5.AddText("Driver");
            para5.Format.LineSpacingRule = LineSpacingRule.OnePtFive;
            para5.AddLineBreak();

            var para6 = addressFrame.AddParagraph();
            para6.AddText(runsheet.Driver);
            para6.Format.LineSpacingRule = LineSpacingRule.Single;
            para6.AddLineBreak();
            para6.AddLineBreak();

            var para7 = addressFrame.AddParagraph();
            para7.Format.Font.Bold = true;
            para7.AddText("Vehicle Details");
            para7.Format.LineSpacingRule = LineSpacingRule.OnePtFive;
            para7.AddLineBreak();

            var para8 = addressFrame.AddParagraph();
            para8.AddText(runsheet.VehicleDetails);
            para8.Format.LineSpacingRule = LineSpacingRule.Single;
            para8.AddLineBreak();
            para8.AddLineBreak();

            foreach (var trip in runsheet.Trips)
            {
                var row = table.AddRow();
                row.TopPadding = 1.5;
                row.Cells[0].AddParagraph(runsheet.Date.ToShortDateString());
                row.Cells[1].AddParagraph(trip.StartTime.ToShortTimeString());
                row.Cells[2].AddParagraph(trip.EndTime.ToShortTimeString());
                row.Cells[3].AddParagraph(trip.Customer);
                var orderString = "";
                foreach (var order in trip.Orders)
                {
                    orderString += order.OrderNumber +" ";
                }
                row.Cells[4].AddParagraph(orderString);
                row.Cells[5].AddParagraph(trip.ReceivedBy);
            }
        }
    }
}