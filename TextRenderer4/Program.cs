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
examBuilder.AddTextLine("Find the result of the following addition : ");
examBuilder.AddQuestion("#QuestionCounter",5, null);
Console.WriteLine(examBuilder.RenderExam());
/*
examBuilder.AddLine("Find the result of the following addition : ");
examBuilder.AddLine("\n");
examBuilder.AddLine("#SerialNumber) ");
examBuilder.AddLine("#RandomInteger$a + #RandomInteger$b");
examBuilder.AddLine("\n");
examBuilder.AddLine("Solution to exercise #SerialNumber : #Result$a$b");
examBuilder.Render();
*/





