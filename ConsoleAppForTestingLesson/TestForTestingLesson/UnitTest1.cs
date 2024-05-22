using ConsoleAppForTestingLesson;

namespace TestForTestingLesson
{
    public class UnitTest1
    {
        private readonly StringCalculator _stringCalculator;

        public UnitTest1()
        {
            _stringCalculator = new();
        }

        [Fact]
        public void Should_Return_Sum_When_String_Contains_Two_Numbers()
        {
            var numbers = "1,2";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(3, result);
        }

        [Fact]
        public void Should_Return_Zero_When_String_Is_Empty()
        {
            var numbers = "";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Should_Return_Number_When_String_Contains_One_Number()
        {
            var numbers = "1";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(1, result);
        }

        [Fact]
        public void Should_Return_Exception_When_String_Contains_Different_Separator_Without_Marker()
        {
            var numbers = "1;2";
            Assert.Throws<FormatException>(() => _stringCalculator.Add(numbers));
        }

        [Fact]
        public void Should_Accept_Different_Char_Separator_With_Marker()
        {
            var numbers = "//;\n1;2;3;4;5";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(15, result);
        }

        [Fact]
        public void Should_Accept_Newline()
        {
            var numbers = "1\n2,3\n4,5";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(15, result);
        }

        [Fact]
        public void Should_Throw_Exception_When_Newline_And_Comma_Are_Adjacent()
        {
            var numbers = "1\n2,3\n4,5\n";
            Assert.Throws<FormatException>(() => _stringCalculator.Add(numbers));
        }

        [Fact]
        public void Should_Throw_Exception_If_There_Are_Negative_Numbers()
        {
            var numbers = "1\n-2,3\n-4,5";
            var exception = Assert.Throws<Exception>(() => _stringCalculator.Add(numbers));
            Assert.Equal("Negatives not allowed: -2, -4", exception.Message);
        }

        [Fact]
        public void Should_Avoid_The_Numbers_That_Are_Highers_Than_1000()
        {
            var numbers = "1\n2,3\n4000,5";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(11, result);
        }

        [Fact]
        public void Should_Accept_Different_Separator_With_Any_Length_With_Marker()
        {
            var numbers = "//[***]\n1***2***3***4000***5";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(11, result);
        }

        [Fact]
        public void Should_Accept_Multiple_Separators_With_Marker()
        {
            var numbers = "//[;][*]\n1;2*3*4000;5";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(11, result);
        }

        [Fact]
        public void Should_Accept_Multiple_Separators_With_Any_Length_With_Marker()
        {
            var numbers = "//[foo][bar]\n1foo2bar3bar4000foo5";
            var result = _stringCalculator.Add(numbers);
            Assert.Equal(11, result);
        }
    }
}