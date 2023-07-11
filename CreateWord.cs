using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;

namespace OOO_Bookstore
{
    class CreateWord
    {
        public Int32 TabRows = 0;
        public Int32 order_num = 0;
        public String Disc = "";
        public String order_usr = "";
        public String Cost = "";
        public String FullPrice = "";
        Int32 RowCell = 2;
        String strcon = "host=localhost;uid=root;password=;database=Bookstore";

        public void Chek(DataGridView dataGridViewOrder)
        {
            Word.Application wdApp = new Word.Application();
            Word.Document wdDoc = null;
            Object wdMiss = System.Reflection.Missing.Value;

            wdDoc = wdApp.Documents.Add(ref wdMiss, ref wdMiss, ref wdMiss, ref wdMiss);

            wdDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;

            Word.Range wordrange = wdDoc.Range(ref wdMiss, ref wdMiss);
            wordrange.PageSetup.LeftMargin = wdApp.CentimetersToPoints(1);
            wordrange.PageSetup.RightMargin = wdApp.CentimetersToPoints(1);
            wordrange.PageSetup.TopMargin = wdApp.CentimetersToPoints(1);
            wordrange.PageSetup.BottomMargin = wdApp.CentimetersToPoints(1);

            wdApp.Visible = true;

            wdApp.ActiveWindow.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            wdApp.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 0.0F;
            wdDoc.PageSetup.PageHeight = 350 + (TabRows * 45);
            wdDoc.PageSetup.PageWidth = 590;

            Word.Paragraph Input;
            Input = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            Input.Range.Font.Name = "Times New Roman";
            Input.Range.Text = "Кассовый чек";
            Input.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Input.Range.Font.Size = Convert.ToInt32(14);
            Input.Range.Font.Bold = 1;
            Input.SpaceAfter = 10;
            Input.Range.InsertParagraphAfter();
            Input.CloseUp();

            Word.Paragraph Countr;
            Countr = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            Countr.Range.Font.Name = "Times New Roman";
            Countr.Range.Text = "ООО 'Книжный магазин'";
            Countr.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Countr.Range.Font.Size = Convert.ToInt32(14);
            Countr.Range.Font.Bold = 0;
            Countr.SpaceAfter = 10;
            Countr.Range.InsertParagraphAfter();
            Countr.CloseUp();

            Word.Paragraph Countr1;
            Countr1 = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            Countr1.Range.Font.Name = "Times New Roman";
            Countr1.Range.Text = "ИНН: 123456789100";
            Countr1.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Countr1.Range.Font.Size = Convert.ToInt32(14);
            Countr1.Range.Font.Bold = 0;
            Countr1.SpaceAfter = 10;
            Countr1.Range.InsertParagraphAfter();
            Countr1.CloseUp();

            Word.Paragraph Countr2;
            Countr2 = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            Countr2.Range.Font.Name = "Times New Roman";
            Countr2.Range.Text = "Адрес: г. Заволжье, ул. Дзержинского, д.28, 729348";
            Countr2.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Countr2.Range.Font.Size = Convert.ToInt32(14);
            Countr2.Range.Font.Bold = 0;
            Countr2.SpaceAfter = 10;
            Countr2.Range.InsertParagraphAfter();
            Countr2.CloseUp();

            Word.Paragraph Order;
            Order = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            Order.Range.Text = "Заказ №" + order_num + " от " + DateTime.Now.ToShortDateString() + " продавец " + order_usr;
            Order.Range.Font.Name = "Times New Roman";
            Order.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Order.Range.Font.Size = Convert.ToInt32(14);
            Order.Range.Font.Bold = 0;
            Order.SpaceAfter = 10;
            Order.Range.InsertParagraphAfter();
            Order.CloseUp();

            Word.Paragraph Order1;
            Order1 = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            Order1.Range.Text = "Продавец: " + order_usr;
            Order1.Range.Font.Name = "Times New Roman";
            Order1.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Order1.Range.Font.Size = Convert.ToInt32(14);
            Order1.Range.Font.Bold = 0;
            Order1.SpaceAfter = 10;
            Order1.Range.InsertParagraphAfter();
            Order1.CloseUp();
            
            Word.Paragraph ForTab;
            ForTab = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            ForTab.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            ForTab.Range.Font.Name = "Times New Roman";
            ForTab.Range.Font.Bold = 1;
            Object defaultTableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
            Object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;

            //Добавляем таблицу и получаем объект wordtable 
            Word.Table wordtable = wdDoc.Tables.Add(ForTab.Range, TabRows + 1, 3, ref defaultTableBehavior, ref autoFitBehavior);
            Word.Range wordcellrange = wdDoc.Tables[1].Cell(0, 0).Range;
            wordcellrange = wordtable.Cell(1, 0).Range;
            wordcellrange.Bold = 1;
            wordcellrange.Text = "Наименование";
            wordcellrange = wordtable.Cell(1, 2).Range;
            wordcellrange.Bold = 1;
            wordcellrange.Text = "Кол-во";
            wordcellrange = wordtable.Cell(1, 3).Range;
            wordcellrange.Bold = 1;
            wordcellrange.Text = "Стоимость";

            MySqlConnection con = new MySqlConnection(strcon);
            con.Open();

            for (Int32 i = 0; i < TabRows; i++)
            {
                wordcellrange = wordtable.Cell(RowCell, 0).Range;
                wordcellrange.Bold = 0;
                wordcellrange.Text = Convert.ToString(dataGridViewOrder.Rows[i].Cells[1].Value);
                wordcellrange = wordtable.Cell(RowCell, 2).Range;
                wordcellrange.Bold = 0;
                wordcellrange.Text = Convert.ToString(dataGridViewOrder.Rows[i].Cells[2].Value);
                wordcellrange = wordtable.Cell(RowCell, 3).Range;
                wordcellrange.Bold = 0;
                wordcellrange.Text = Convert.ToString(dataGridViewOrder.Rows[i].Cells[4].Value);
                RowCell++;
            }

            Word.Paragraph Price;
            Price = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            Price.Range.Text = "Стоимость: " + Cost;
            Price.Range.Font.Name = "Times New Roman";
            Price.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            Price.Range.Font.Size = Convert.ToInt32(14);
            Price.Range.Font.Bold = 0;
            Price.SpaceBefore = 10;
            Price.SpaceAfter = 10;
            Price.Range.InsertParagraphAfter();
            Price.CloseUp();

            Word.Paragraph Discount;
            Discount = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            Discount.Range.Text = "Скидка: " + Disc;
            Discount.Range.Font.Name = "Times New Roman";
            Discount.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            Discount.Range.Font.Size = Convert.ToInt32(14);
            Discount.Range.Font.Bold = 0;
            Discount.SpaceAfter = 10;
            Discount.Range.InsertParagraphAfter();
            Discount.CloseUp();

            Word.Paragraph fPrice;
            fPrice = wdDoc.Content.Paragraphs.Add(ref wdMiss);
            fPrice.Range.Text = "Итого: " + FullPrice;
            fPrice.Range.Font.Name = "Times New Roman";
            fPrice.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            fPrice.Range.Font.Size = Convert.ToInt32(14);
            fPrice.Range.Font.Bold = 0;
            fPrice.SpaceAfter = 10;
            fPrice.Range.InsertParagraphAfter();
            fPrice.CloseUp();
        }   //создание чека в Word
    }
}
