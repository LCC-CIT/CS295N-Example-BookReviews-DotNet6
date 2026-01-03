using BookReviews;
using BookReviews.Models;
using Xunit;
using System;

namespace BookReviewTests
{
    public class QuizTests
    {
        [Fact]
        public void CheckAnswerRightTest()
        {
            // arrange: 
            var set = Quiz.GenerateQuestionSet();
            // set all the answers to the right answer
            foreach(var answer in set)
            {
                answer.UserAnswer = answer.Answer;
            }

            // act
            Quiz.CheckAnswers(set);

            // assert
            bool result = true;  // assume all the answers were right
            // change result if ANY answer is wrong
            foreach(var answer in set)
            {   
                result = result && (answer.IsRight ?? false);  // count null as false
            }
            Assert.True(result);
        }

        [Fact]
        public void CheckAnswerWrongTest()
        {
            // arrange: 
            var set = Quiz.GenerateQuestionSet();
            // set all the answers to the wrong answer
            foreach (var answer in set)
            {
                answer.UserAnswer = "Something random";
            }

            // act
            Quiz.CheckAnswers(set);

            // assert
            bool result = false;  // assume all the answers were wrong
            // change result if ANY answer is right
            foreach (var answer in set)
            {
                result = result || (answer.IsRight ?? false);  // count null as false
            }
            Assert.False(result);
        }

        [Fact]
        public void CheckAnswerPartialCorrectTest()
        {
            // arrange
            var set = Quiz.GenerateQuestionSet();
            // Set half the answers right, half wrong
            for (int i = 0; i < set.Count; i++)
            {
                if (i % 2 == 0)
                    set[i].UserAnswer = set[i].Answer;
                else
                    set[i].UserAnswer = "Wrong answer";
            }

            // act
            Quiz.CheckAnswers(set);

            // assert
            int correctCount = 0;
            foreach (var answer in set)
            {
                if (answer.IsRight == true)
                    correctCount++;
            }
            Assert.Equal(5, correctCount); // Exactly half should be correct
        }

        [Fact]
        public void CheckAnswerCaseSensitivityTest()
        {
            // arrange
            var set = Quiz.GenerateQuestionSet();
            // Find a short answer question and use lowercase
            var victorHugoQuestion = set.Find(q => q.Answer == "Victor Hugo");
            victorHugoQuestion.UserAnswer = "victor hugo"; // lowercase

            // act
            Quiz.CheckAnswers(set);

            // assert
            // This test documents current behavior - case matters
            Assert.False(victorHugoQuestion.IsRight);
        }

        [Fact]
        public void GenerateQuestionSetConsistencyTest()
        {
            // act
            var set1 = Quiz.GenerateQuestionSet();
            var set2 = Quiz.GenerateQuestionSet();

            // assert
            Assert.Equal(10, set1.Count);
            Assert.Equal(10, set2.Count);
            // Questions should be identical each time
            for (int i = 0; i < set1.Count; i++)
            {
                Assert.Equal(set1[i].Question, set2[i].Question);
                Assert.Equal(set1[i].Answer, set2[i].Answer);
                Assert.Equal(set1[i].Type, set2[i].Type);
            }
        }

        [Fact]
        public void QuestionVM_IsRightInitialStateTest()
        {
            // arrange & act
            var question = new QuestionVM
            {
                Type = QuestionType.ShortAnswer,
                Question = "Test?",
                Answer = "Test"
            };

            // assert
            Assert.Null(question.IsRight); // Should be null before checking
        }

        [Fact]
        public void CheckAnswersWithNullUserAnswerTest()
        {
            // arrange
            var set = Quiz.GenerateQuestionSet();
            set[0].UserAnswer = null;

            // act
            Quiz.CheckAnswers(set);

            // assert
            Assert.False(set[0].IsRight);
        }

        [Fact]
        public void GenerateQuestionSet_VerifyQuestionTypesTest()
        {
            // act
            var set = Quiz.GenerateQuestionSet();

            // assert
            int shortAnswerCount = 0;
            int numericCount = 0;
            int trueFalseCount = 0;

            foreach (var q in set)
            {
                switch (q.Type)
                {
                    case QuestionType.ShortAnswer:
                        shortAnswerCount++;
                        break;
                    case QuestionType.Numeric:
                        numericCount++;
                        break;
                    case QuestionType.TrueFalse:
                        trueFalseCount++;
                        break;
                }
            }

            Assert.Equal(4, shortAnswerCount);
            Assert.Equal(2, numericCount);
            Assert.Equal(4, trueFalseCount);
        }

        [Fact]
        public void Book_PublisherCanBeNullTest()
        {
            // arrange & act
            var book = new Book
            {
                BookId = 1,
                BookTitle = "Test Book",
                AuthorName = "Test Author",
                Isbn = 123456,
                Publisher = null, // Nullable property
                PubDate = DateTime.Now
            };

            // assert
            Assert.Null(book.Publisher);
            Assert.NotNull(book.BookTitle);
        }

        [Fact]
        public void Review_ModelRelationshipsTest()
        {
            // arrange
            var book = new Book { BookId = 1, BookTitle = "Test" };
            var user = new AppUser { AppUserId = 1, UserName = "Tester" };
            var reviewDate = DateTime.Parse("2024-01-15");

            // act
            var review = new Review
            {
                ReviewId = 1,
                Book = book,
                Reviewer = user,
                ReviewText = "Great book!",
                ReviewDate = reviewDate
            };

            // assert
            Assert.NotNull(review.Book);
            Assert.NotNull(review.Reviewer);
            Assert.Equal("Test", review.Book.BookTitle);
            Assert.Equal("Tester", review.Reviewer.UserName);
            Assert.Equal(reviewDate, review.ReviewDate);
        }
    }
}
