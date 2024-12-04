using System;
using System.Collections.Generic;
using TextRenderer2;




public class ExamBuilder  {
    private Exam m_exam; // product
    public Exam MExam => m_exam;

    List<QuestionBuilder> m_questions = new List<QuestionBuilder>();

    public ExamBuilder() {
        m_exam = new Exam();
    }

    public static ExamBuilder Create() {
        return new ExamBuilder();
    }
    
    public ExamBuilder Question(Func<QuestionBuilder,QuestionBuilder> question) {
        var qbuilder = new QuestionBuilder();
        var questionBuilder = question(qbuilder);
        m_questions.Add(questionBuilder);   
        return this;
    }

    public ExamBuilder EndExam() {
        return this;
    }
}



public class QuestionBuilder  {
    
    private Question m_question;
    public Question MQuestion => m_question;

    List<QuestionBuilder> m_questions = new List<QuestionBuilder>();

    public QuestionBuilder() {
        
    }

    public QuestionBuilder Question(Func<QuestionBuilder, QuestionBuilder> question) {
        var qbuilder = new QuestionBuilder();
        var questionBuilder = question(qbuilder);
        m_questions.Add(questionBuilder);
        return this;
    }

    
}

