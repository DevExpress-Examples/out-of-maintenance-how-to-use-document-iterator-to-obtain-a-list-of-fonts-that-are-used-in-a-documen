using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentIteratorExample
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
            ribbonControl1.SelectedPage = pageIterator;
            richEditControl1.LoadDocument("TextWithDifferentFonts.docx");
        }

        private void btnIteratorRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region #runvisitor
            MyVisitor visitor = new MyVisitor();
            DocumentIterator iterator = new DocumentIterator(richEditControl1.Document, true);
            while (iterator.MoveNext())
                iterator.Current.Accept(visitor);
            listBoxControl1.DataSource = visitor.DocumentFonts;
            #endregion #runvisitor
        }
    }
    #region #myvisitorclass
    public class MyVisitor : DocumentVisitorBase
    {
        private List<string> fonts;
        public List<string> DocumentFonts {get {return fonts;}}
        public MyVisitor()
        {
            fonts = new List<string>();
        }

        public override void Visit(DocumentText text) {
            if (!fonts.Contains(text.TextProperties.FontName))
                fonts.Add(text.TextProperties.FontName);
        }
    }
    #endregion #myvisitorclass
}
