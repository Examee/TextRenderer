// See https://aka.ms/new-console-template for more information


using System.Text;
using Randomize;
using TextRenderer3;
using TextRenderer4;

CExamBuilder examBuilder = new CExamBuilder();




examBuilder.AddExam((scopesystem) => {
    SerialPeaker serialPeaker = new SerialPeaker();
    scopesystem.AddMacroToCurrentScope("QuestionCounter", (_) =>
        serialPeaker.NextSerial().ToString());
});


// Creates a new question with a unique ID in the exam scope
examBuilder.AddQuestion("#QuestionCounter$A",5,
    (scopesystem) => {
        SerialPeaker serialPeaker = new SerialPeaker();
        scopesystem.AddMacroToCurrentScope("SubQuestionCounter", (_) =>
            serialPeaker.NextSerial().ToString());

        CRandomIntegerContextMemory integerContext = new CRandomIntegerContextMemory();
        scopesystem.AddMacroToCurrentScope("RandomInteger", (parameters) =>
            integerContext.GetNextRandomNumber(parameters[0]).ToString());
    });

examBuilder.AddText("Find the result of the following addition : ",CQuestion.TEXT);
examBuilder.AddNewLine(CQuestion.TEXT);
examBuilder.AddText("\t#SubQuestionCounter$a) ", CQuestion.TEXT);
examBuilder.AddText("#RandomInteger$a + #RandomInteger$b", CQuestion.TEXT);
examBuilder.AddNewLine( CQuestion.TEXT);
examBuilder.AddSolution("&A", null);
examBuilder.AddText("Solution to exercise #QuestionCounter$A : #Result$a$b",CSolution.TEXT);
Console.WriteLine(examBuilder.RenderExam());
/*



examBuilder.AddLine("Solution to exercise #SerialNumber : #Result$a$b");
examBuilder.Render();
*/





