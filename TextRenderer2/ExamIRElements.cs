using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRenderer2 {
    public class ExameeIRElement {

        public enum IRElementType {
            NA=-1,Exam,Question,Solution,Text
        }
        private IRElementType mType= IRElementType.NA;
        public IRElementType MType {
            get => mType;
        }

        private ExameeIRElement mParent;
        public ExameeIRElement Parent {
            get => mParent;
        }

        public ExameeIRElement(ExameeIRElement parent,
            IRElementType type) {
            mParent = parent;
            mType = type;
        } 
    }

    public class ExameeCompositeIRElement : ExameeIRElement {
        private Dictionary<int, List<ExameeIRElement>> m_contents;

        public ExameeCompositeIRElement(ExameeIRElement parent, 
            IRElementType type) :
            base(parent, type) {
            m_contents = new Dictionary<int, List<ExameeIRElement>>();
        }
        public void AddContent(int index, ExameeIRElement element) {
            if (!m_contents.ContainsKey(index)) {
                m_contents[index] = new List<ExameeIRElement>();
            }
            m_contents[index].Add(element);
        }
    }

    public class ExameeTextIRElement : ExameeIRElement {
        public string Text { get; set; }
        public ExameeTextIRElement(ExameeIRElement parent) :
            base(parent, IRElementType.Text) {
        }
    }

    public class Exam : ExameeCompositeIRElement {
        public Exam() : base(null, IRElementType.Exam) {
        }
    }

    public class Question : ExameeCompositeIRElement {
        public Question(ExameeIRElement parent) : 
            base(parent, IRElementType.Question) {
        }
    }

    public class Solution : ExameeCompositeIRElement {
        public Solution(ExameeIRElement parent) :
            base(parent, IRElementType.Solution) {
        }
    }
}
