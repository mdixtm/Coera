using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using TestProject.Models;
using TestProject.Validators;

namespace UnitTests
{
    [TestFixture]
    public class CallTimeValidationTests
    {
        [Test]
        public void Validation_Should_Pass_When_Times_Are_Valid()
        {
            var candidate = new Candidate
            {
                CallTimeStart = new TimeSpan(9, 0, 0),
                CallTimeEnd = new TimeSpan(17, 0, 0)
            };

            var validationContext = new ValidationContext(candidate) { MemberName = "CallTimeEnd" };
            var attribute = new CallTimeEndValidation("CallTimeStart");

            var result = attribute.GetValidationResult(candidate.CallTimeEnd, validationContext);

            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        public void Validation_Should_Fail_When_EndTime_Before_StartTime()
        {
            var candidate = new Candidate
            {
                CallTimeStart = new TimeSpan(17, 0, 0),
                CallTimeEnd = new TimeSpan(9, 0, 0)
            };

            var validationContext = new ValidationContext(candidate) { MemberName = "CallTimeEnd" };
            var attribute = new CallTimeEndValidation("CallTimeStart");

            var result = attribute.GetValidationResult(candidate.CallTimeEnd, validationContext);

            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result.ErrorMessage, Is.EqualTo("Call end time must be after start time"));
        }

        [Test]
        public void Validation_Should_Pass_When_Times_Are_Null()
        {
            var candidate = new Candidate
            {
                CallTimeStart = null,
                CallTimeEnd = null
            };

            var validationContext = new ValidationContext(candidate) { MemberName = "CallTimeEnd" };
            var attribute = new CallTimeEndValidation("CallTimeStart");

            var result = attribute.GetValidationResult(candidate.CallTimeEnd, validationContext);

            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        public void Validation_Should_Fail_When_Only_StartTime_Is_Provided()
        {
            var candidate = new Candidate
            {
                CallTimeStart = new TimeSpan(9, 0, 0),
                CallTimeEnd = null
            };

            var validationContext = new ValidationContext(candidate) { MemberName = "CallTimeEnd" };
            var attribute = new CallTimeEndValidation("CallTimeStart");

            var result = attribute.GetValidationResult(candidate.CallTimeEnd, validationContext);

            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result.ErrorMessage, Is.EqualTo("Call end time is required if start time is provided"));
        }

        [Test]
        public void Validation_Should_Fail_When_Only_EndTime_Is_Provided()
        {
            var candidate = new Candidate
            {
                CallTimeStart = null,
                CallTimeEnd = new TimeSpan(17, 0, 0)
            };

            var validationContext = new ValidationContext(candidate) { MemberName = "CallTimeEnd" };
            var attribute = new CallTimeEndValidation("CallTimeStart");

            var result = attribute.GetValidationResult(candidate.CallTimeEnd, validationContext);

            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result.ErrorMessage, Is.EqualTo("Call start time is required if end time is provided"));
        }
    }
}
