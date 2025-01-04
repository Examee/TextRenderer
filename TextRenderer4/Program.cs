// See https://aka.ms/new-console-template for more information


using System.Text;
using TextRenderer3;

CExamBuilder examBuilder = new CExamBuilder();




examBuilder.AddExam((scopesystem) => {
    SerialPeaker serialPeaker = new SerialPeaker();
    scopesystem.AddMacroToCurrentScope("QuestionCounter", (_) =>
        serialPeaker.NextSerial().ToString());
});

examBuilder.AddQuestion("#QuestionCounter",5,null);
examBuilder.AddQuestion("#QuestionCounter",5, null);
/*
examBuilder.AddLine("Find the result of the following addition : ");
examBuilder.AddLine("\n");
examBuilder.AddLine("#SerialNumber) ");
examBuilder.AddLine("#RandomInteger$a + #RandomInteger$b");
examBuilder.AddLine("\n");
examBuilder.AddLine("Solution to exercise #SerialNumber : #Result$a$b");
examBuilder.Render();
*/




public class CExam : CExamCompositeBlock {
    public const int QUESTION = 0 , SOLUTION = 1;


    public CExam() : base(2,null) {
        
    }

    public void AddQuestion(CQuestion question) {
        AddBlock(question, QUESTION);
    }
}

public class CQuestion : CExamCompositeBlock {
    public const int TEXT = 0, QUESTION_SET = 1;

    private string m_questionId;
    public string MQuestionId => m_questionId;

    public CQuestion(string questionID,CExamBlock parent) : base(2,parent) {
        m_questionId= questionID;
    }
}

public class CText : CExamBlock {
    StringBuilder m_text = new StringBuilder();
    public CText(CExamBlock parent) : base(parent) {
    }

    internal override void AddBlock(CExamBlock block, int type=0) {

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
