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



examBuilder.AddQuestion("#QuestionCounter",5,
    (scopesystem) => {
        SerialPeaker serialPeaker = new SerialPeaker();
        scopesystem.AddMacroToCurrentScope("SubQuestionCounter", (_) =>
            serialPeaker.NextSerial().ToString());

        RandomContext context = new RandomContext();
        scopesystem.AddMacroToCurrentScope("RandomInteger", (parameters) =>
            context.GetNextRandomNumber(parameters[0]).ToString());
    });

examBuilder.AddText("Find the result of the following addition : ");
examBuilder.AddNewLine();
examBuilder.AddText("\t#SubQuestionCounter) ");
examBuilder.AddText("#RandomInteger$a + #RandomInteger$b");
examBuilder.AddNewLine();
examBuilder.AddQuestion("#QuestionCounter",5, null);
Console.WriteLine(examBuilder.RenderExam());
/*



examBuilder.AddLine("Solution to exercise #SerialNumber : #Result$a$b");
examBuilder.Render();
*/





