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
    public class MyVisitor : ParentVisitor
    {
        public List<string> DocumentFonts {get {return Fonts;}}
        public override void Visit(DocumentText text) {
            if (!Fonts.Contains(text.TextProperties.FontName))
                Fonts.Add(text.TextProperties.FontName);
        }
    }
    #endregion #myvisitorclass

    #region #parentvisitorclass
    public abstract class ParentVisitor : IDocumentVisitor
    {
        private List<string> fonts;
        protected List<string> Fonts { get { return fonts; } set { fonts = value; } }
        protected ParentVisitor()
        {
            fonts = new List<string>();
        }
        public virtual void Visit(DocumentTableCellBorder cellBorder) { }
        public virtual void Visit(DocumentSectionStart sectionStart) { }
        public virtual void Visit(DocumentFieldCodeStart fieldCodeStart) { }
        public virtual void Visit(DocumentFieldCodeEnd fieldCodeEnd) { }
        public virtual void Visit(DocumentFieldResultEnd fieldResultEnd) { }
        public virtual void Visit(DocumentBookmarkStart bookmarkStart) { }
        public virtual void Visit(DocumentBookmarkEnd bookmarkEnd) { }
        public virtual void Visit(DocumentCommentStart commentStart) { }
        public virtual void Visit(DocumentSectionEnd sectionEnd) { }
        public virtual void Visit(DocumentCommentEnd commentEnd) { }
        public virtual void Visit(DocumentRangePermissionStart rangePermissionStart) { }
        public virtual void Visit(DocumentRangePermissionEnd rangePermissionEnd) { }
        public virtual void Visit(DocumentTextBox textBox) { }
        public virtual void Visit(DocumentText text) { }
        public virtual void Visit(DocumentParagraphStart paragraphStart) { }
        public virtual void Visit(DocumentParagraphEnd paragraphEnd) { }
        public virtual void Visit(DocumentInlinePicture inlinePicture) { }
        public virtual void Visit(DocumentPicture picture) { }
        public virtual void Visit(DocumentHyperlinkStart hyperlinkStart) { }
        public virtual void Visit(DocumentHyperlinkEnd hyperlinkEnd) { }
    }
    #endregion #parentvisitorclass
}
