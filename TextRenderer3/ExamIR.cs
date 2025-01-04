using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRenderer3 {
    public abstract class CExamBlock {
        public CExamBlock MParent { get; }
        
        public CExamBlock(CExamBlock mParent) {
            MParent = mParent;
        }
        internal abstract void AddBlock(CExamBlock block, int type = 0);

        public abstract string GetContent();
    }

    public class CExamCompositeBlock : CExamBlock {
        // The content of the block is stored in an array of lists where the
        // key is the context and the value is a list of blocks
        private List<CExamBlock>[] m_content;
        public int MNumberOfContexts { get; }

        public CExamCompositeBlock(int numContexts, CExamBlock parent) : base(parent) {
            m_content = new List<CExamBlock>[numContexts];
            MNumberOfContexts = numContexts;
        }

        internal override void AddBlock(CExamBlock block, int context) {
            if (context < m_content.Length) {
                if (m_content[context] == null) {
                    m_content[context] = new List<CExamBlock>();
                }
                m_content[context].Add(block);
            } else {
                throw new System.Exception("Invalid context");
            }
        }

        public override string GetContent() {
            StringBuilder content = new StringBuilder();
            for (int context = 0; context < m_content.Length; context++) {
                content.Append(GetContextContent(context));
            }
            return content.ToString();
        }

        public string GetContextContent(int context) {
            StringBuilder content = new StringBuilder();
            foreach (CExamBlock block in m_content[context]) {
                content.Append(block.ToString());
            }
            return content.ToString();
        }
    }
}
