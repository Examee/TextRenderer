using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRenderer4;

namespace TextRenderer3 {
    // Represents a block of text in the exam
    public abstract class CExamBlock {
        public CExamBlock MParent { get; }

        public CScope MScope { get; internal set; }
        
        public CExamBlock(CExamBlock mParent) {
            MParent = mParent;
        }
        
        public abstract string GetContent();

        public T GetParent<T>() where T:class {
            CExamBlock block = this;
            while (block.MParent !=null &&
                   block.MParent is T) {
                block = block.MParent;
            }
            return block as T;
        }
    }

    public class CExamCompositeBlock : CExamBlock {
        // The content of the block is stored in an array of lists where the
        // key is the context and the value is a list of blocks
        private List<CExamBlock>[] m_content;
        public int MNumberOfContexts { get; }

        public CExamCompositeBlock(int numContexts, CExamBlock parent) : base(parent) {
            m_content = new List<CExamBlock>[numContexts];
            for (int i = 0; i < numContexts; i++) {
                m_content[i] = new List<CExamBlock>();
            }
            MNumberOfContexts = numContexts;
        }

        public override string GetContent() {
            StringBuilder content = new StringBuilder();
            for (int context = 0; context < m_content.Length; context++) {
                content.Append(GetContextContent(context));
            }
            return content.ToString();
        }

        internal virtual void AddBlock(CExamBlock block, int context) {
            if (context < m_content.Length) {
                if (m_content[context] == null) {
                    m_content[context] = new List<CExamBlock>();
                }
                m_content[context].Add(block);
            } else {
                throw new System.Exception("Invalid context");
            }
        }

        protected string GetContextContent(int context) {
            StringBuilder content = new StringBuilder();
            foreach (CExamBlock block in m_content[context]) {
                content.Append(block.GetContent());
            }
            return content.ToString();
        }
    }

    public class CText : CExamBlock {
        StringBuilder m_text = new StringBuilder();
        public CText(CExamCompositeBlock parent, int context) : base(parent) {
            parent.AddBlock(this, context);
        }

        public void AddLine(string text) {
            m_text.AppendLine(text);
        }
        public void AddText(string text) {
            m_text.Append(text);
        }
        public void AddNewLine() {
            m_text.AppendLine();
        }
        public override string GetContent() {
            return m_text.ToString();
        }

    }
}
