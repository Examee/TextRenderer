using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRenderer3;

namespace TextRenderer4 {
    public class CExam : CExamCompositeBlock {
        public const int QUESTION = 0, SOLUTION = 1;
        public CExam() : base(2, null) {
        }

        public void AddQuestion(CQuestion question) {
            AddBlock(question, QUESTION);
        }
    }

    public class CQuestion : CExamCompositeBlock {
        public const int TEXT = 0, QUESTION_SET = 1;

        private string m_questionId;
        public string MQuestionId => m_questionId;

        public CQuestion(string questionID, CExam parent) : base(2, parent) {
            m_questionId = questionID;
            parent.AddBlock(this, CExam.QUESTION);
        }

        public override string GetContent() {
            StringBuilder content = new StringBuilder();
            content.Append(GetContextContent(QUESTION_SET) + ") ");
            content.Append(GetContextContent(TEXT));
            return content.ToString();
        }
    }

    public class CSolution : CExamCompositeBlock {
        public const int TEXT = 0, SOLUTION_SET = 1;
        public CSolution(CExam parent) : base(2, parent) {
            parent.AddBlock(this, CExam.SOLUTION);
        }
        public override string GetContent() {
            StringBuilder content = new StringBuilder();
            content.Append(GetContextContent(SOLUTION_SET) + ") ");
            content.Append(GetContextContent(TEXT));
            return content.ToString();
        }
    }
}
