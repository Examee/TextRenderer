// See https://aka.ms/new-console-template for more information


using System.Text;
using Randomize;
using TextRenderer3;

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

examBuilder.AddText("Find the result of the following addition : ");
examBuilder.AddNewLine();
examBuilder.AddText("\t#SubQuestionCounter$a) ");
examBuilder.AddText("#RandomInteger$a$b[0][1] + #RandomInteger$b[0][1]");
examBuilder.AddNewLine();
examBuilder.AddSolution("A", null);
examBuilder.AddText("Solution to exercise #QuestionCounter$A : #Result$a$b");
Console.WriteLine(examBuilder.RenderExam());
/*



examBuilder.AddLine("Solution to exercise #SerialNumber : #Result$a$b");
examBuilder.Render();
*/





